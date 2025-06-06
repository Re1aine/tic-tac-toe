using VContainer;

public class StartRoundState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IGameplayFactory _gameFactory;
    private readonly ICameraService _cameraService;
    private readonly IObjectResolver _objectResolver;

    public StartRoundState(GameplayStateMachine gameplayStateMachine,
        IGameplayFactory gameFactory,
        ICameraService cameraService,
        IObjectResolver objectResolver)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameFactory = gameFactory;
        _cameraService = cameraService;
        _objectResolver = objectResolver;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _gameFactory.CreatePlayer();
        _gameFactory.CreateGrid();
        _gameFactory.CreateSimpleBot();
        
        _objectResolver.Resolve<RoundStateTracker>(); 
        _objectResolver.Resolve<EndRoundHandler>();   
        _objectResolver.Resolve<RoundStateHandler>(); 
        
        _gameplayStateMachine.Enter<GameplayLoopState>();
    }
    
    public void Exit()
    {
        
    }
}