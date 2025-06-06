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
    private readonly IGameplaySceneProvider _gameplaySceneProvider;

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
        IBombSpawner bombSpawner,
        IGameplaySceneProvider gameplaySceneProvider)
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
        _gameplaySceneProvider = gameplaySceneProvider;
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
        
        _coroutineRunner.StartCoroutine(AnimateRestart(grid), CoroutineScopes.Gameplay);
    }

    private IEnumerator AnimateGameFieldRestart(float duration, float angle)
    {
        Transform gameField = _gameplaySceneProvider.GameField.transform;

        var startRotation = gameField.rotation;
        var endRotation = startRotation * Quaternion.Euler(0, angle, 0);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            gameField.rotation = Quaternion.Lerp(
                startRotation, 
                endRotation, 
                Mathf.SmoothStep(0, 1, progress)
            );
        
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        gameField.rotation = endRotation;
    }

    private IEnumerator AnimateRestart(GameGrid grid)
    {
        yield return _coroutineRunner.StartCoroutine(AnimateGameFieldRestart(2, 180),CoroutineScopes.Gameplay);
        
        int size = grid.Grid.GetLength(0);
        float delayBetweenCells = 0.3f;
        
        for (int layer = 0; layer < size * 2 - 1; layer++)
        {
            int startRow = Mathf.Min(layer, size - 1);
            int startCol = Mathf.Max(0, layer - size + 1);

            List<Cell> currentLayerCells = new List<Cell>();
            for (int i = 0; i <= Mathf.Min(startRow, size - 1 - startCol); i++)
            {
                int row = startRow - i;
                int col = startCol + i;
                currentLayerCells.Add(grid.Grid[row, col]);
            }

            foreach (var cell in currentLayerCells) 
                cell.Clear();

            yield return new WaitForSeconds(delayBetweenCells);
        }
        
        OnComplete();
    }

    private void OnComplete()
    {
        _pauseService.UnPause();
        _hudProvider.GameplayUI.ResetTimer();

        _gameplayStateMachine.Enter<GameplayLoopState>();
    }
    
    public void Exit()
    {
        
    }
}