using System.Collections.Generic;
using System.Linq;

public class BombsHolder : IBombsHolder
{
    public IReadOnlyList<Bomb> Bombs => _bombs;
    private readonly List<Bomb> _bombs = new();
        
    public void Add(Bomb bomb) => _bombs.Add(bomb);
    public void Remove(Bomb bomb) => _bombs.Remove(bomb);
    public void DestroyAll()
    {
        _bombs.ToList().ForEach(x => x.Destroy());
        _bombs.Clear();
    }
}

public interface IBombsHolder
{
    IReadOnlyList<Bomb> Bombs { get; }
    void Add(Bomb bomb);
    void Remove(Bomb bomb);
    void DestroyAll();
}