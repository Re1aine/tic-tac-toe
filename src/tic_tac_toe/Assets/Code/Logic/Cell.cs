using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IRaycastable
{
    public event Action<CellState> StateChanged;
    public int X { get; private set; }
    public int Y { get; private set; }

    public CellState State;

    [SerializeField] private Material _circle;
    [SerializeField] private Material _cross;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _originMaterial;
    private readonly List<Figure> _figures = new();

    private void Awake() => _originMaterial = _meshRenderer.material.color;

    public void Init(int x, int y)
    {
        X = x;
        Y = y;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Figure figure))
        {
            _meshRenderer.material.color = figure.FigureType == FigureType.Circle ? _circle.color : _cross.color;
            _figures.Add(figure);

            State = CellState.X;
            StateChanged?.Invoke(State);
        }
    }
    
private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Figure figure))
        {
            _figures.Remove(figure);
            
            if (IsEmpty())
            {
                State = CellState.Empty;
                _meshRenderer.material.color = _originMaterial;
                
                StateChanged?.Invoke(State);
            }
        }
    }
    private bool IsEmpty() => _figures.Count == 0;
}

public enum CellState
{
    Empty = 0,
    X = 1,
    O = 2,
}

