using VContainer.Unity;

public class ProjectEntryPoint : IInitializable
{
    private readonly GameStateMachine _gameStateMachine;

    public ProjectEntryPoint(GameStateMachine gameStateMachine) => 
        _gameStateMachine = gameStateMachine;

    public void Initialize() => _gameStateMachine.Enter<InitializeState>();
}
