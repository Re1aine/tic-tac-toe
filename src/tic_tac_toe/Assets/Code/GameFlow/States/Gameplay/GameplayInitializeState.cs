public class GameplayInitializeState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IProjectileFactory _projectileFactory;
    private readonly IFigureFactory _figureFactory;
    private readonly IHUDProvider _hudProvider;

    public GameplayInitializeState(GameplayStateMachine gameplayStateMachine,
        IProjectileFactory projectileFactory,
        IFigureFactory figureFactory,
        IHUDProvider hudProvider)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _projectileFactory = projectileFactory;
        _figureFactory = figureFactory;
        _hudProvider = hudProvider;
    }

    public void Enter()
    {
        _projectileFactory.Initialize();
        _figureFactory.Initialize();
        _hudProvider.Initialize();
        
        _gameplayStateMachine.Enter<StartRoundState>();
    }

    public void Exit()
    {
        
    }
}