using System.Collections;
using UnityEngine;
using VContainer;

public class Tank : MonoBehaviour, IPausable
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootForce;
    [SerializeField] private int _coolDown = 3;
    
    private IProjectileFactory _projectileFactory;
    private IPauseService _pauseService;

    private bool _isPaused;
    
    [Inject]
    public void Construct(IProjectileFactory projectileFactory, IPauseService pauseService)
    {
        _projectileFactory = projectileFactory;
        _pauseService = pauseService;
    }
    
    public void Start()
    {
        _pauseService.Add(this);
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if(_isPaused) return;
        
        transform.RotateAround(Vector3.zero, Vector3.up, 5 * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSecondsUnpaused(_pauseService, _coolDown);
            
            var randomRotation = RandomHelper.GetRandomRotation();
            var projectile = _projectileFactory.CreateProjectile(_shootPoint.position, randomRotation);
            projectile.Launch(_shootPoint.transform.forward, _shootForce);
        }
    }

    public void Pause() => _isPaused = true;

    public void UnPause() => _isPaused = false;

    public void OnDestroy() => _pauseService.Remove(this);
}