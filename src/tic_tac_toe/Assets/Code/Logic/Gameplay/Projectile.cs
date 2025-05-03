using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IRaycastable
{
    public event Action Destroyed;

    private Rigidbody _rigidbody;
    private float _lifetime;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            Destroyed?.Invoke();
    }

    public void SetLifetime(float lifetime) =>
        _lifetime = lifetime;

    public void ResetRigidbody()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    public void Launch(Vector3 direction, float force) => 
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
}