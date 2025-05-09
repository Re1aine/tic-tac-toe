using VContainer;

public class StartRoundState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly ICameraService _cameraService;
    private readonly IObjectResolver _objectResolver;
    private readonly IUIFactory _uiFactory;

    public StartRoundState(GameplayStateMachine gameplayStateMachine,
        IGameFactory gameFactory,
        ICameraService cameraService,
        IObjectResolver objectResolver,
        IUIFactory uiFactory)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameFactory = gameFactory;
        _cameraService = cameraService;
        _objectResolver = objectResolver;
        _uiFactory = uiFactory;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _gameFactory.CreatePlayer();
        _gameFactory.CreateGrid();
        _uiFactory.CreateHud();
        
        _objectResolver.Resolve<RoundStateTracker>(); 
        _objectResolver.Resolve<EndRoundHandler>();   
        _objectResolver.Resolve<RoundStateHandler>(); 
        
        _gameplayStateMachine.Enter<GameplayLoopState>();
    }
    
    public void Exit()
    {
        
    }
}