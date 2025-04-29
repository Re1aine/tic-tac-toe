public interface IState : IExitableState
{
    void Enter();
}

public interface IStateWithArg<TArg> : IExitableState
{
    void Enter(TArg arg);
}

public interface IExitableState
{
    void Exit();
}