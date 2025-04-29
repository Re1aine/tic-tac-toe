using VContainer;

public class ContainerProvider : IContainerProvider
{ 
    public IObjectResolver Container { get; }

    public ContainerProvider(IObjectResolver container) => 
        Container = container;
}

public interface IContainerProvider
{
    IObjectResolver Container { get; }
}

