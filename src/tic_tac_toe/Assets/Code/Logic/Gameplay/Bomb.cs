using UnityEngine;

public class Bomb : MonoBehaviour, IRaycastable
{
    private const int BufferSize = 32;
    
    [SerializeField] private LayerMask _affectedLayers;
    [SerializeField] private float _timeToExplode;
    [SerializeField] private float _radiusExplode;
    [SerializeField] private int _forceExplode;
    [SerializeField] private int _upWardsForce;

    private Collider[] _explosionHits;
    private bool _isExploded;

    private void Update()
    {
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
                _explosionHits[i].attachedRigidbody.AddExplosionForce(_forceExplode, transform.position, _radiusExplode,
                    _upWardsForce,
                    ForceMode.Impulse
                );
            }
        }
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusExplode);
    }
}