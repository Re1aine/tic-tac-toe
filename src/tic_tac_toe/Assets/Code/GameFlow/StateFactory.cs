using VContainer;

public class StateFactory
{
    private readonly IObjectResolver _resolver;
    
    public StateFactory(IObjectResolver resolver) => 
        _resolver = resolver;
    
    public TState Create<TState>() where TState : IExitableState => 
        _resolver.ResolveInstance<TState>();
}

