using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGrid
{
    public event Action GridStateChanged;
    public Cell[,] Grid => _grid;

    private readonly Cell[,] _grid = new Cell[3,3];
    private readonly IGameplaySceneProvider _sceneProvider;
    
    public GameGrid(IGameplaySceneProvider sceneProvider) => 
        _sceneProvider = sceneProvider;

    public void Initialize()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                
                Transform cellTransform = _sceneProvider.GameField.GameQuads.GetChild(index);
                Cell cell = cellTransform.GetComponent<Cell>();
                
                _grid[x, y] = cell;
                
                cell.Init(x, y);
                cellTransform.name = $"Cell_{x}_{y}";
                
                cell.StateChanged += OnGridStateChanged;
            }
        }
    }

    public Cell GetRandomCell()
    {
        int x = Random.Range(0, _grid.GetLength(0));
        int y = Random.Range(0, _grid.GetLength(1));
        return _grid[x, y];
    }

    private void OnGridStateChanged(CellState cellState)
    {
        GridStateChanged?.Invoke();
    }
}