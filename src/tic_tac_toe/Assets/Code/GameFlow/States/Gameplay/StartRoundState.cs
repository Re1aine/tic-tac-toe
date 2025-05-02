using VContainer;

public class StartRoundState : IState
{
    private readonly IGameFactory _gameFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICameraService _cameraService;
    private readonly IObjectResolver _objectResolver;

    public StartRoundState(
        IGameFactory gameFactory,
        ICoroutineRunner coroutineRunner,
        ICameraService cameraService,
        IObjectResolver objectResolver)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _cameraService = cameraService;
        _objectResolver = objectResolver;
    }

    public void Enter()
    {
        _cameraService.Activate();
        _gameFactory.CreatePlayer();
        _gameFactory.CreateGrid();
        
        _objectResolver.Resolve<RoundStateTracker>(); 
        _objectResolver.Resolve<EndRoundHandler>();   
        _objectResolver.Resolve<RoundStateHandler>(); 
    }
    
    public void Exit()
    {
        
    }
}