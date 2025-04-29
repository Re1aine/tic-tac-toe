using UnityEngine;
using VContainer;

public class StartRoundState : IState
{
    private readonly IGameFactory _gameFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICameraService _cameraService;
    private readonly IContainerProvider _containerProvider;

    public StartRoundState(
        IGameFactory gameFactory,
        ICoroutineRunner coroutineRunner,
        ICameraService cameraService,
        IContainerProvider containerProvider)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _cameraService = cameraService;
        _containerProvider = containerProvider;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _gameFactory.CreatePlayer();
        _gameFactory.CreateGrid();
        
        _containerProvider.Container.Resolve<RoundStateTracker>(); 
        _containerProvider.Container.Resolve<EndRoundHandler>();   
        _containerProvider.Container.Resolve<RoundStateHandler>(); 
    }
    
    public void Exit()
    {
        
    }
}