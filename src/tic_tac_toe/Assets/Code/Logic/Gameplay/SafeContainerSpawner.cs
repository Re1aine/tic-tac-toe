using System.Collections;
using UnityEngine;
using VContainer;

public class SafeContainerSpawner : ISafeContainerSpawner
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IGameplayFactory _gameFactory;
    private readonly IObjectResolver _objectResolver;
    private readonly IPauseService _pauseService;

    private Coroutine _coroutine;
    
    public SafeContainerSpawner(ICoroutineRunner coroutineRunner,
        IGameplayFactory gameFactory,
        IObjectResolver objectResolver,
        IPauseService pauseService)
    {
        _coroutineRunner = coroutineRunner;
        _gameFactory = gameFactory;
        _objectResolver = objectResolver;
        _pauseService = pauseService;
    }

    public void Enable()
    {
        var grid = _objectResolver.Resolve<GameGrid>();
        _coroutine = _coroutineRunner.StartCoroutine(SpawnSafeContainers(grid), CoroutineScopes.Gameplay);
    }

    public void Disable() => 
        _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

    private IEnumerator SpawnSafeContainers(GameGrid grid)
    {
        while (true)
        {
            yield return new WaitForSecondsUnpaused(_pauseService, 5);
            
            var targetCell = grid.GetRandomCell();
            
            Vector3 spawnPosition = targetCell.transform.position + Vector3.down * 5f;
            var safeContainer = _gameFactory.CreateSafeContainer(spawnPosition);
            
            Vector3 targetPosition = targetCell.transform.position + Vector3.up * 2;
            
            yield return safeContainer.StartMoveTo(spawnPosition, targetPosition, 5);
            yield return new WaitForSecondsUnpaused(_pauseService, 5);
            yield return safeContainer.StartMoveTo(targetPosition, spawnPosition, 5);
            
            safeContainer.Destroy();
        }
    }
}

public interface ISafeContainerSpawner
{
    void Enable();
    void Disable();
}

