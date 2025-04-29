using VContainer.Unity;

public class GameplayEntryPoint : IInitializable
{
    private readonly GameplayStateMachine _gameStateMachine;
    private readonly IContainerProvider _containerProvider;
    private readonly IGameFactory _gameFactory;

    public GameplayEntryPoint(GameplayStateMachine gameStateMachine, IContainerProvider containerProvider, IGameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _containerProvider = containerProvider;
        _gameFactory = gameFactory;
    }

    public void Initialize()
    {
        _gameStateMachine.Enter<StartRoundState>();
    }
}