using System.Collections.Generic;
using System.Linq;

public class ProjectilesHolder : IProjectilesHolder
{
    public IReadOnlyList<Projectile> Projectiles => _projectiles;
    
    private readonly List<Projectile> _projectiles = new();
    
    public void Add(Projectile projectile) => _projectiles.Add(projectile);
    public void Remove(Projectile projectile) => _projectiles.Remove(projectile);
    public void DestroyAll()
    {
        _projectiles.ToList().ForEach(x => x.Destroy());
        _projectiles.Clear();
    }
}

public interface IProjectilesHolder
{
    IReadOnlyList<Projectile> Projectiles { get; }
    void Add(Projectile projectile);
    void Remove(Projectile projectile);
    void DestroyAll();
}