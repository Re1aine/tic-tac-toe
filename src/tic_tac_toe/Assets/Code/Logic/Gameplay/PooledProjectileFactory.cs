using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PooledProjectileFactory : IProjectileFactory
{
    private const string ParentName = "Projectiles";
    
    private readonly IObjectResolver _resolver;
    private readonly IProjectilesHolder _projectilesHolder;
    private ObjectPool<Projectile> _projectilePool;
    private Projectile _projectilePrefab;
    private Transform _projectilesParent;
    
    private readonly float _lifeTimeProjectile = 5f;

    public PooledProjectileFactory(IObjectResolver resolver, IProjectilesHolder projectilesHolder)
    {
        _resolver = resolver;
        _projectilesHolder = projectilesHolder;
    }

    public void Initialize()
    {
        _projectilePool = new ObjectPool<Projectile>(CreateNewProjectile, OnGetProjectile, OnReleaseProjectile,
            null, false, 5, 7);

        _projectilePrefab = Resources.Load<Projectile>("Projectile");
        _projectilesParent = new GameObject(ParentName).transform;

        for (int i = 0; i < 5; i++) _projectilePool.Release(CreateNewProjectile());
    }

    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        var projectile = _projectilePool.Get();
        projectile.transform.position = position;
        projectile.SetLifetime(_lifeTimeProjectile);
        projectile.ResetRigidbody();
        
        _projectilesHolder.Add(projectile);
        
        return projectile;
    }

    private Projectile CreateNewProjectile()
    {
        var projectile = _resolver.Instantiate(_projectilePrefab, _projectilesParent);
        projectile.Destroyed += () => _projectilePool.Release(projectile);
        return projectile;
    }

    private void OnGetProjectile(Projectile projectile) => 
        projectile.gameObject.SetActive(true);

    private void OnReleaseProjectile(Projectile projectile) => 
        projectile.gameObject.SetActive(false);
}

public interface IProjectileFactory
{
    Projectile CreateProjectile(Vector3 position, Quaternion rotation);
    void Initialize();
}