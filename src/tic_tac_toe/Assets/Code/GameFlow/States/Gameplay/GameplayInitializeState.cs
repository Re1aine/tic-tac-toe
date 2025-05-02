public class GameplayInitializeState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IProjectileFactory _projectileFactory;
    private readonly IFigureFactory _figureFactory;

    public GameplayInitializeState(GameplayStateMachine gameplayStateMachine,
        IProjectileFactory projectileFactory,
        IFigureFactory figureFactory)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _projectileFactory = projectileFactory;
        _figureFactory = figureFactory;
    }

    public void Enter()
    {
        _projectileFactory.Initialize();
        _figureFactory.Initialize();
        
        _gameplayStateMachine.Enter<StartRoundState>();
    }

    public void Exit()
    {
        
    }
}