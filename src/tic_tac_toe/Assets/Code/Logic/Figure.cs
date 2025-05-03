using System;
using Unity.VisualScripting;
using UnityEngine;

public class Figure : MonoBehaviour, IRaycastable
{
    public event Action Destroyed;
    public FigureType FigureType => _figureType;
    
    [SerializeField] private FigureType _figureType;
    
    private Rigidbody _rigidbody;

    private float _lifetime;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();
    
    public void SetLifeTime(float lifetime) 
        => _lifetime = lifetime;

    public void ResetRigidbody()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }
    
    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            Destroyed?.Invoke();
    }

    public void Destroy() => Destroyed?.Invoke();
}

public enum FigureType
{
    Circle = 0,
    Cross = 1
}