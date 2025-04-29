using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameFactory : IGameFactory
{
    private readonly IContainerProvider _containerProvider;

    public GameFactory(IContainerProvider containerProvider) =>
        _containerProvider = containerProvider;

    public GameGrid CreateGrid()
    {
        var grid = _containerProvider.Container.Resolve<GameGrid>();
        grid.Initialize();
    
        return grid;
    }
    
    public Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation) =>
        AssetProvider.InstantiateAt<Figure>(type.ToString(), position, rotation);
    
    public Player CreatePlayer()
    {
        var prefab = Resources.Load<Player>("Player");
        return _containerProvider.Container.Instantiate(prefab);
    }
}

public interface IGameFactory
{
    Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation);
    Player CreatePlayer();
    GameGrid CreateGrid();
}

