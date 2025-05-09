using System.Collections.Generic;
using System.Linq;

public class ProjectilesHolder : IProjectilesHolder
{
    public IReadOnlyList<Projectile> Projectiles => _projectiles;
    
    private readonly List<Projectile> _projectiles = new();
    
    public void Add(Projectile projectile) => _projectiles.Add(projectile);
    public void Remove(Projectile projectile) => _projectiles.Remove(projectile);
    public void DestroyAll() => _projectiles.ToList().ForEach(x => x.Destroy());
}

public interface IProjectilesHolder
{
    IReadOnlyList<Projectile> Projectiles { get; }
    void Add(Projectile projectile);
    void Remove(Projectile projectile);
    void DestroyAll();
}

public class SafeContainersHolder : ISafeContainersHolder
{
    public IReadOnlyList<SafeContainer> SafeContainers => _safeContainers;
    
    private readonly List<SafeContainer> _safeContainers = new();
    
    public void Add(SafeContainer safeContainer) => _safeContainers.Add(safeContainer);
    public void Remove(SafeContainer safeContainer) => _safeContainers.Remove(safeContainer);
    public void DestroyAll() => _safeContainers.ToList().ForEach(x => x.Destroy());
}

public interface ISafeContainersHolder
{
    IReadOnlyList<SafeContainer> SafeContainers { get; }
    void Add(SafeContainer safeContainer);
    void Remove(SafeContainer safeContainer);
    void DestroyAll();
}