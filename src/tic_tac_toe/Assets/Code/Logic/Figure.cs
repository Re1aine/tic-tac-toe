using System;
using UnityEngine;

public class Figure : MonoBehaviour, IRaycastable, IPausable
{
    public event Action Destroyed;
    
    [field: SerializeField] public FigureType FigureType { get; private set; }
    [field: SerializeField] public FigureModificator FigureModificator { get; set; } = FigureModificator.None;
    
    private Rigidbody _rigidbody;

    private float _lifetime;
    private bool _isPaused;

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
        if(_isPaused) return;
        
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            Destroyed?.Invoke();
    }

    public void Destroy() => Destroyed?.Invoke();
    
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
}

public enum FigureType
{
    Circle = 0,
    Cross = 1
}

public enum FigureModificator
{
    None = 0,
    UnForcable = 1,
}