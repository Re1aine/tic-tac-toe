using System.Collections.Generic;
using System.Linq;

public class FiguresHolder : IFiguresHolder
{
    public IReadOnlyList<Figure> Figures => _figures;
    
    private readonly List<Figure> _figures = new();
    public void Add(Figure figure) => _figures.Add(figure);
    public void Remove(Figure figure) => _figures.Remove(figure);

    public void DestroyAll()
    {
        _figures.ToList().ForEach(x => x.Destroy());
        _figures.Clear();
    }
}

public interface IFiguresHolder
{
    IReadOnlyList<Figure> Figures { get; }
    void Add(Figure figure);
    void Remove(Figure figure);
    void DestroyAll();
}