using System.Collections;
using UnityEngine;

public class MenuState : IState
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICameraService _cameraService;
    private readonly IMenuSceneProvider _sceneProvider;
    private readonly IGameFactory _gameFactory;

    public MenuState(ICoroutineRunner coroutineRunner,
        ICameraService cameraService,
        IMenuSceneProvider sceneProvider,
        IGameFactory gameFactory)
    {
        _coroutineRunner = coroutineRunner;
        _sceneProvider = sceneProvider;
        _gameFactory = gameFactory;
        _cameraService = cameraService;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _coroutineRunner.StartCoroutine(SpawnFigure(), CoroutineScopes.Menu);
    }

    public void Exit()
    {
        
    }
    
    private IEnumerator SpawnFigure()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            
            var randomType = RandomHelper.GetRandomEnumValue<FigureType>();
            var randomRotation = RandomHelper.GetRandomRotation();
            var randomPos = RandomHelper.GetRandomPositionInCollider(_sceneProvider.SpawnZone,
                false, 
                true, 
                false);
            _gameFactory.CreateFigure(FigureType.Cross, randomPos, randomRotation);
        }
    }
}