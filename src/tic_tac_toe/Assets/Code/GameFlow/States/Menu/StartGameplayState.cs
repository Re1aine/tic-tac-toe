public class StartGameplayState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;

    public StartGameplayState(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        _coroutineRunner.StopCoroutines(CoroutineScopes.Menu);
        _gameStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Gameplay);
    }

    public void Exit()
    {
        
    }
}