using UnityEngine;
using VContainer;

public class GameplayPooledFigureFactory : PooledFigureFactory
{
    private readonly IFiguresHolder _figuresHolder;

    public GameplayPooledFigureFactory(IObjectResolver resolver, IFiguresHolder figuresHolder) : base(resolver)
    {
        _figuresHolder = figuresHolder;
    }

    public override Figure CreateFigure(Vector3 position, Quaternion rotation)
    {
        var figure =  base.CreateFigure(position, rotation);
        _figuresHolder.Add(figure);
        return figure;
    }
}