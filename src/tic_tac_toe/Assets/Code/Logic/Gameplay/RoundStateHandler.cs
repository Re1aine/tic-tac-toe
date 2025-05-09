
public class RoundStateHandler
{
    private readonly GameplayStateMachine _gameStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;
    
    private readonly EndRoundHandler _endRoundHandler;

    public RoundStateHandler(GameplayStateMachine gameStateMachine, EndRoundHandler endRoundHandler)
    {
        _gameStateMachine = gameStateMachine;

        _endRoundHandler = endRoundHandler;

        _endRoundHandler.RoundEnd += EndRound;
    }
    
    public void StartRound()
    {
        _gameStateMachine.Enter<StartRoundState>();
    }

    public void PauseRound()
    {
        _gameStateMachine.Enter<PauseRoundState>();
    }

    private void EndRound(RoundState result)
    { 
        _gameStateMachine.Enter<EndRoundState, RoundState>(result); 
    }

    public void ExitRound()
    {
        _gameStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu);
    }

    public void RestartRound()
    {
        _gameStateMachine.Enter<RestartRoundState>();
    }
}