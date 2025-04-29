public class ExitGameplayState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;

    public ExitGameplayState(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        _coroutineRunner.StopCoroutines(CoroutineScopes.Gameplay);
        _gameStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu);
    }

    public void Exit()
    {
        
    }
}