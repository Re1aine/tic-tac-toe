using System.Collections;
using UnityEngine;
using VContainer;

public class StartRoundState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICameraService _cameraService;
    private readonly IObjectResolver _objectResolver;
    private readonly IGameplaySceneProvider _gameplaySceneProvider;

    public StartRoundState(GameplayStateMachine gameplayStateMachine,
        IGameFactory gameFactory,
        ICoroutineRunner coroutineRunner,
        ICameraService cameraService,
        IObjectResolver objectResolver,
        IGameplaySceneProvider gameplaySceneProvider)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _cameraService = cameraService;
        _objectResolver = objectResolver;
        _gameplaySceneProvider = gameplaySceneProvider;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _gameFactory.CreatePlayer();
        _gameFactory.CreateGrid();
        
        _objectResolver.Resolve<RoundStateTracker>(); 
        _objectResolver.Resolve<EndRoundHandler>();   
        _objectResolver.Resolve<RoundStateHandler>(); 
        
        _gameplayStateMachine.Enter<GameplayLoopState>();
    }
    
    public void Exit()
    {
        
    }
}