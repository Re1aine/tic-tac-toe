using UnityEngine;

public class EndRoundState : IStateWithArg<RoundState>
{
    private readonly GameplayStateMachine _gameStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;

    public EndRoundState(GameplayStateMachine gameStateMachine, ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter(RoundState result)
    {
        _gameStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu);
    }

    public void Exit()
    {
        Debug.Log("END ROUND STATE");
    }
}