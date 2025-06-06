using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IRaycastable
{
    public event Action<CellState> StateChanged;
    
    public CellState State;
    public int X { get; private set; }
    public int Y { get; private set; }

    [SerializeField] private Material _circle;
    [SerializeField] private Material _cross;
    [SerializeField] private MeshRenderer _meshRenderer;

    private readonly List<Figure> _figures = new();
    private Color _originMaterial;
    private bool _isActive;

    private void Awake() => _originMaterial = _meshRenderer.material.color;

    private void Start() => _isActive = true;

    public void Init(int x, int y)
    {
        X = x;
        Y = y;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!_isActive) return;

        if (other.gameObject.TryGetComponent(out Figure figure))
        {
            _meshRenderer.material.color = figure.FigureType == FigureType.Circle ? _circle.color : _cross.color;
            _figures.Add(figure);
            figure.Disabled += OnFigureActiveDisabled;

            State = CellState.X;

            StateChanged?.Invoke(State);
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if(!_isActive) return;
        
        if (other.gameObject.TryGetComponent(out Figure figure))
        {
            _figures.Remove(figure);
            figure.Disabled -= OnFigureActiveDisabled;

            
            if (IsEmpty())
            {
                State = CellState.Empty;
                _meshRenderer.material.color = _originMaterial;
                
                StateChanged?.Invoke(State);
            }
        }
    }

    private void OnFigureActiveDisabled() => 
        _meshRenderer.material.color = _originMaterial;

    private bool IsEmpty() => _figures.Count == 0;

    public void Clear()
    {
        StartCoroutine(ClearAnimation());
    }

    private IEnumerator ClearAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 loweredPosition = startPosition + Vector3.down * 5f;
        float duration = 0.5f;
    
        yield return MoveTo(loweredPosition, duration);
    
        yield return RotateAroundZ(180, duration * 2);
    
        yield return MoveTo(startPosition, duration);
    }

    private IEnumerator MoveTo(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    
        transform.position = targetPosition;
    }

    private IEnumerator RotateAroundZ(float degrees, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, degrees);
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    
        transform.rotation = endRotation;
    }
}

public enum CellState
{
    Empty = 0,
    X = 1,
    O = 2,
}

