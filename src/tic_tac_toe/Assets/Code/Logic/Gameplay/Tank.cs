using System.Collections;
using UnityEngine;
using VContainer;

public class Tank : MonoBehaviour, IPausable
{
    [SerializeField] private Transform _shootPoint;
    
    private IGameplayFactory _gameplayFactory;
    private IPauseService _pauseService;
    private IStaticDataService _staticDataService;

    private bool _isPaused;
    
    [Inject]
    public void Construct(IGameplayFactory gameplayFactory, IPauseService pauseService, IStaticDataService staticDataService)
    {
        _gameplayFactory = gameplayFactory;
        _pauseService = pauseService;
        _staticDataService = staticDataService;
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
            yield return new WaitForSecondsUnpaused(_pauseService, _staticDataService.TankStaticData.Cooldown);
            
            var randomRotation = RandomHelper.GetRandomRotation();
            var projectile = _gameplayFactory.CreateProjectile(_shootPoint.position, randomRotation);
            projectile.Launch(_shootPoint.transform.forward, _staticDataService.TankStaticData.ShootForce);
        }
    }

    public void Pause() => _isPaused = true;
    public void UnPause() => _isPaused = false;
    public void Destroy() => _pauseService.Remove(this);
}
