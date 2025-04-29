
public class InitializeState : IState
{
    private readonly GameStateMachine _stateMachine;
    
    public InitializeState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        _stateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu);
    }

    public void Exit()
    {
       
    }
}
