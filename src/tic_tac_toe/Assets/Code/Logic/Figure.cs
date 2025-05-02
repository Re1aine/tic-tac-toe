using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public event Action Destroyed;
    public FigureType FigureType => _figureType;
    
    [SerializeField] private FigureType _figureType;
    
    private float _lifetime;
    
    public void SetLifeTime(float lifetime) 
        => _lifetime = lifetime;

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

