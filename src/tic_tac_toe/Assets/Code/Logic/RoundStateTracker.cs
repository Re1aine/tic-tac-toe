using System;
using UnityEngine;

public class RoundStateTracker : IDisposable
{
    public event Action RoundStateChanged;
    public RoundState State { get; private set; } = RoundState.InProgress;
    
    private readonly GameGrid _gameGrid;

    public RoundStateTracker(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;

        _gameGrid.GridStateChanged += SetRoundState;
        Debug.Log("THIS IS THE ROUNDSTATE TRACKER");
    }
    
    private void SetRoundState()
    {
        if (IsWinState(CellState.X)) State = RoundState.CrossWin;
        else if (IsWinState(CellState.O)) State = RoundState.CircleWin;
        else State = RoundState.InProgress;
        
        RoundStateChanged?.Invoke();
    }
    
    private bool IsWinState(CellState state)
    {
        for (int i = 0; i < 3; i++)
        {
            if (_gameGrid.Grid[0, i].State == state && 
                _gameGrid.Grid[1, i].State == state && 
                _gameGrid.Grid[2, i].State == state)
                return true;
            
            if (_gameGrid.Grid[i, 0].State == state && 
                _gameGrid.Grid[i, 1].State == state && 
                _gameGrid.Grid[i, 2].State == state)
                return true;
        }
        
        if (_gameGrid.Grid[0, 0].State == state && 
            _gameGrid.Grid[1, 1].State == state && 
            _gameGrid.Grid[2, 2].State == state)
            return true;
        
        if (_gameGrid.Grid[2, 0].State == state && 
            _gameGrid.Grid[1, 1].State == state && 
            _gameGrid.Grid[0, 2].State == state)
            return true;
        
        return false;
    }

    public void Dispose()
    {
        _gameGrid.GridStateChanged -= SetRoundState;
    }
}

public enum RoundState
{
    CircleWin = 0,
    CrossWin = 1,
    InProgress = 2
}