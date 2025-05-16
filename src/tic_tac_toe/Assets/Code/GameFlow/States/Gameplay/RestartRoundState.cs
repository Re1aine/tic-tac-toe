using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class RestartRoundState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IObjectResolver _resolver;
    private readonly IHUDProvider _hudProvider;
    private readonly IProjectilesHolder _projectilesHolder;
    private readonly IFiguresHolder _figuresHolder;
    private readonly ISafeContainersHolder _safeContainersHolder;
    private readonly IBombsHolder _bombHolder;
    private readonly IPauseService _pauseService;
    private readonly ISafeContainerSpawner _safeContainerSpawner;
    private readonly IBombSpawner _bombSpawner;

    public RestartRoundState(GameplayStateMachine gameplayStateMachine,
        ICoroutineRunner coroutineRunner,
        IObjectResolver resolver,
        IHUDProvider hudProvider,
        IProjectilesHolder projectilesHolder,
        IFiguresHolder figuresHolder,
        ISafeContainersHolder safeContainersHolder,
        IBombsHolder bombHolder,
        IPauseService pauseService,
        ISafeContainerSpawner safeContainerSpawner,
        IBombSpawner bombSpawner)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _coroutineRunner = coroutineRunner;
        _resolver = resolver;
        _hudProvider = hudProvider;
        _projectilesHolder = projectilesHolder;
        _figuresHolder = figuresHolder;
        _safeContainersHolder = safeContainersHolder;
        _bombHolder = bombHolder;
        _pauseService = pauseService;
        _safeContainerSpawner = safeContainerSpawner;
        _bombSpawner = bombSpawner;
    }

    public void Enter()
    {
        var grid = _resolver.Resolve<GameGrid>();
        
        _figuresHolder.DestroyAll();
        _projectilesHolder.DestroyAll();
        _safeContainersHolder.DestroyAll();
        _bombHolder.DestroyAll();
        
        _safeContainerSpawner.Disable();
        _bombSpawner.Disable();
        
        _coroutineRunner.StartCoroutine(WaveClearAnimation(grid), CoroutineScopes.Gameplay);
    }

    private IEnumerator WaveClearAnimation(GameGrid grid)
    {
        int size = grid.Grid.GetLength(0); // Предполагаем квадратную сетку
        float delayBetweenCells = 0.3f;

        // Проходим по всем диагональным слоям
        for (int layer = 0; layer < size * 2 - 1; layer++)
        {
            // Определяем стартовые позиции для текущего слоя
            int startRow = Mathf.Min(layer, size - 1);
            int startCol = Mathf.Max(0, layer - size + 1);

            // Собираем все клетки в текущем диагональном слое
            List<Cell> currentLayerCells = new List<Cell>();
            for (int i = 0; i <= Mathf.Min(startRow, size - 1 - startCol); i++)
            {
                int row = startRow - i;
                int col = startCol + i;
                currentLayerCells.Add(grid.Grid[row, col]);
            }

            // Запускаем анимацию для всех клеток слоя одновременно
            foreach (var cell in currentLayerCells)
            {
                cell.Clear();
            }

            // Ждем перед следующим слоем
            yield return new WaitForSeconds(delayBetweenCells);
        }
        
        Completed();
    }

    private void Completed()
    {
        _pauseService.UnPause();
        _hudProvider.GameplayUI.ResetTimer();

        _gameplayStateMachine.Enter<GameplayLoopState>();
    }
    
    public void Exit()
    {
        
    }
}