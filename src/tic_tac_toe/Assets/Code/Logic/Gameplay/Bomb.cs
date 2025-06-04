using System;
using UnityEngine;
using VContainer;

public class Bomb : MonoBehaviour, IRaycastable, IPausable
{
    public event Action<Bomb> Destroyed;
    
    private const int BufferSize = 32;
    
    [SerializeField] private LayerMask _affectedLayers;
    [SerializeField] private float _timeToExplode;
    [SerializeField] private float _radiusExplode;
    [SerializeField] private int _forceExplode;
    [SerializeField] private int _upWardsForce;

    private IPauseService _pauseService;
    
    private Rigidbody _rigidbody;
    private Collider[] _explosionHits;
    
    private bool _isExploded;
    private bool _isPaused;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    [Inject]
    public void Construct(IPauseService pauseService)
    {
        _pauseService = pauseService;
    }

    private void Start() => _pauseService.Add(this);

    private void Update()
    {
        if (_isPaused) return;
        
        if (_isExploded) return;
        
        _timeToExplode -= Time.deltaTime;
        if (_timeToExplode <= 0)
            Explode();
    }

    private void Explode()
    {
        if (_isExploded) return;
        _isExploded = true;
        
        _explosionHits = new Collider[BufferSize];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radiusExplode, _explosionHits, _affectedLayers);
        
        for (int i = 0; i < hitCount; i++)
        {
            if (_explosionHits[i] != null && _explosionHits[i].attachedRigidbody != null)
            {
                if (_explosionHits[i].gameObject.TryGetComponent<Figure>(out var figure) &&
                    figure.FigureModificator == FigureModificator.UnForcable)
                    continue;
                
                _explosionHits[i].attachedRigidbody.AddExplosionForce(_forceExplode, transform.position, _radiusExplode,
                    _upWardsForce,
                    ForceMode.Impulse
                );
            }
        }
        Destroy();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusExplode);
    }

    public void Pause()
    {
        _isPaused = true;
        _rigidbody.isKinematic = true;
    }

    public void UnPause()
    {
        _isPaused = false;
        _rigidbody.isKinematic = false;
    }
    
    public void Destroy()
    {
        Destroyed?.Invoke(this);
        _pauseService.Remove(this);
        Destroy(gameObject);
    }
}