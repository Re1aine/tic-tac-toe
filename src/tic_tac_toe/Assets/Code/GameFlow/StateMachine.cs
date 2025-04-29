
namespace Code.GameFlow
{
    public abstract class StateMachine 
    {
        private readonly StateFactory _stateFactory;
        private IExitableState _currentState;

        protected StateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state?.Enter();
        }

        public void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArg<TArg>
        {
            TState state = ChangeState<TState>();
            state.Enter(arg);
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _stateFactory.Create<TState>();

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            TState state = GetState<TState>();
        
            _currentState?.Exit();
            _currentState = state;

            return state;
        }
    }
}