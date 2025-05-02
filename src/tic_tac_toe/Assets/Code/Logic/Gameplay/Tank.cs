using System.Collections;
using UnityEngine;
using VContainer;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootForce;
    [SerializeField] private int _coolDown = 3;
    
    private IProjectileFactory _projectileFactory;

    [Inject]
    public void Construct(IProjectileFactory projectileFactory)
    {
        _projectileFactory = projectileFactory;
    }
    
    public void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 5 * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            var randomRotation = RandomHelper.GetRandomRotation();
            var projectile = _projectileFactory.CreateProjectile(_shootPoint.position, randomRotation);
            projectile.Launch(_shootPoint.transform.forward, _shootForce);
            yield return new WaitForSeconds(_coolDown);
        }
    }
}

