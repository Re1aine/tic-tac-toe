using UnityEngine;
using VContainer;

public abstract class GameFactory : IGameFactory
{
    protected readonly IObjectResolver _objectResolver;
    private readonly IFigureFactory _figureFactory;

    protected GameFactory(IObjectResolver objectResolver, IFigureFactory figureFactory)
    {
        _objectResolver = objectResolver;
        _figureFactory = figureFactory;
    }
    
    public Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation) => 
        _figureFactory.CreateFigure(type, position, rotation);
}

public interface IGameFactory
{
    Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation);
}

