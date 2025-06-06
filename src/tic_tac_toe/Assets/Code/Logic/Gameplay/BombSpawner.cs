using System.Collections;
using UnityEngine;

public class BombSpawner : IBombSpawner
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IGameplayFactory _gameFactory;
    private readonly IGameplaySceneProvider _gameplaySceneProvider;
    private readonly IPauseService _pauseService;

    private Coroutine _coroutine;
    
    private readonly int _bombCount = 1;
    
    public BombSpawner(ICoroutineRunner coroutineRunner,
        IGameplayFactory gameFactory,
        IGameplaySceneProvider gameplaySceneProvider,
        IPauseService pauseService)
    {
        _coroutineRunner = coroutineRunner;
        _gameFactory = gameFactory;
        _gameplaySceneProvider = gameplaySceneProvider;
        _pauseService = pauseService;
    }

    public void Enable() => 
        _coroutine = _coroutineRunner.StartCoroutine(SpawnBombs(), CoroutineScopes.Gameplay);

    public void Disable() => 
        _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            yield return new WaitForSecondsUnpaused(_pauseService, 3f);
            
            for (int i = 0; i < _bombCount; i++)
            {
                _gameFactory.CreateBomb(
                    RandomHelper.GetRandomPositionInCollider(_gameplaySceneProvider.GameField.SpawnZone),
                    RandomHelper.GetRandomRotation());
            }
        }
    }

}

public interface IBombSpawner
{
    void Enable();
    void Disable();
}