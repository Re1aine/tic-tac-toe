using VContainer.Unity;

public class GameplayEntryPoint : IInitializable
{
    private readonly GameplayStateMachine _gameStateMachine;

    public GameplayEntryPoint(GameplayStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
        _gameStateMachine.Enter<GameplayInitializeState>();
    }
}