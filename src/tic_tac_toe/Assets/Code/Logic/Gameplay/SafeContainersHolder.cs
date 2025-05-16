using System.Collections.Generic;
using System.Linq;

public class SafeContainersHolder : ISafeContainersHolder
{
    public IReadOnlyList<SafeContainer> SafeContainers => _safeContainers;
    
    private readonly List<SafeContainer> _safeContainers = new();
    
    public void Add(SafeContainer safeContainer) => _safeContainers.Add(safeContainer);
    public void Remove(SafeContainer safeContainer) => _safeContainers.Remove(safeContainer);
    public void DestroyAll()
    {
        _safeContainers.ToList().ForEach(x => x.Destroy());
        _safeContainers.Clear();
    }
}

public interface ISafeContainersHolder
{
    IReadOnlyList<SafeContainer> SafeContainers { get; }
    void Add(SafeContainer safeContainer);
    void Remove(SafeContainer safeContainer);
    void DestroyAll();
}