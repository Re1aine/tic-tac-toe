using VContainer;

public class MenuFactory : GameFactory, IMenuFactory
{
    public MenuFactory(IObjectResolver objectResolver, IFigureFactory figureFactory)
        : base(objectResolver, figureFactory)
    {
        
    }
}

public interface IMenuFactory : IGameFactory
{
    
}