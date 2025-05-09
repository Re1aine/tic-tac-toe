using UnityEngine;
using VContainer;
using VContainer.Unity;

public  class GameFactory : IGameFactory
{
    private readonly IObjectResolver _objectResolver;

    protected GameFactory(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
    }

    public GameGrid CreateGrid()
    {
        var grid = _objectResolver.Resolve<GameGrid>();
        grid.Initialize();
        return grid;
    }

  
    
    public Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation)
    {
        return AssetProvider.InstantiateAt<Figure>(type.ToString(), position, rotation);
    }

    public Bomb CreateBomb(Vector3 position, Quaternion rotation)
    {
        var prefab = Resources.Load<Bomb>("Bomb");
        return _objectResolver.Instantiate(prefab, position, rotation);
    }

    public Player CreatePlayer()
    {
        var prefab = Resources.Load<Player>("Player");
        return _objectResolver.Instantiate(prefab);
    }

    public SafeContainer CreateSafeContainer(Vector3 position)
    {
        var prefab = Resources.Load<SafeContainer>("SafeContainer");
        return _objectResolver.Instantiate(prefab, position, Quaternion.identity);
    }
}

public interface IGameFactory
{
    Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation);
    Player CreatePlayer();
    GameGrid CreateGrid();
    Bomb CreateBomb(Vector3 position, Quaternion rotation);
    SafeContainer CreateSafeContainer(Vector3 position);
}

