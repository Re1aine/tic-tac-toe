using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IRaycastable, IPausable
{
    public event Action Destroyed;
    
    private Rigidbody _rigidbody;
    private Material _material;

    private Vector3 _velocity;
    private float _lifetime;
    private bool _isPaused;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        if(_isPaused) return;
        
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

    public void SetColor(Color color) => _material.color = color;

    public void Launch(Vector3 direction, float force) => 
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);

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

    public void Destroy() => Destroyed?.Invoke();
}