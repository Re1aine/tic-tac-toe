
using Code.GameFlow;

public sealed class GameStateMachine : StateMachine
{
    public GameStateMachine(StateFactory stateFactory) : base(stateFactory)
    {
    }
}