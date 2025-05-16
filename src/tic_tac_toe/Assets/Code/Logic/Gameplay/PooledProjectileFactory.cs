using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PooledProjectileFactory : IProjectileFactory
{
    private const string ParentName = "Projectiles";
    
    private readonly IObjectResolver _resolver;
    private readonly IProjectilesHolder _projectilesHolder;
    private readonly IPauseService _pauseService;
    private readonly IStaticDataService _staticDataService;
    
    private ObjectPool<Projectile> _projectilePool;
    private Transform _projectilesParent;
    
    public PooledProjectileFactory(IObjectResolver resolver, IProjectilesHolder projectilesHolder, IPauseService pauseService,
         IStaticDataService staticDataService)
    {
        _resolver = resolver;
        _projectilesHolder = projectilesHolder;
        _pauseService = pauseService;
        _staticDataService = staticDataService;
    }

    public void Initialize()
    {
        _projectilePool = new ObjectPool<Projectile>(CreateNewProjectile, OnGetProjectile, OnReleaseProjectile,
            null, false, 5, 7);
        
        _projectilesParent = new GameObject(ParentName).transform;
        for (int i = 0; i < 5; i++) _projectilePool.Release(CreateNewProjectile());
    }

    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        var data = _staticDataService.ProjectileStaticData;
        
        var projectile = _projectilePool.Get();
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.SetLifetime(data.Lifetime);
        projectile.SetColor(data.Color);
        projectile.ResetRigidbody();

        return projectile;
    }

    private Projectile CreateNewProjectile()
    {
        var randomPrefab = _staticDataService.ProjectileStaticData.prefabs.GetRandomGameObject();
        var projectile = _resolver.Instantiate(randomPrefab, _projectilesParent)
            .GetComponent<Projectile>();
        
        projectile.Destroyed += () => _projectilePool.Release(projectile);
        return projectile;
    }

    private void OnGetProjectile(Projectile projectile)
    {
        _projectilesHolder.Add(projectile);
        _pauseService.Add(projectile);
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectile(Projectile projectile)
    {
        _projectilesHolder.Remove(projectile);
        _pauseService.Remove(projectile);
        projectile.gameObject.SetActive(false);
    }
}

public interface IProjectileFactory
{
    Projectile CreateProjectile(Vector3 position, Quaternion rotation);
    void Initialize();
}