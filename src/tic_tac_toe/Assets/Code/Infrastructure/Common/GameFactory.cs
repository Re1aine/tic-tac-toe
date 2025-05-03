using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameFactory : IGameFactory
{
    private readonly IObjectResolver _objectResolver;

    public GameFactory(IObjectResolver objectResolver) =>
        _objectResolver = objectResolver;

    public GameGrid CreateGrid()
    {
        var grid = _objectResolver.Resolve<GameGrid>();
        grid.Initialize();
    
        return grid;
    }
    
    public Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation) =>
        AssetProvider.InstantiateAt<Figure>(type.ToString(), position, rotation);
    
    public Player CreatePlayer()
    {
        var prefab = Resources.Load<Player>("Player");
        return _objectResolver.Instantiate(prefab);
    }

    public Bomb CreateBomb(Vector3 position, Quaternion rotation) => 
        AssetProvider.InstantiateAt<Bomb>("Bomb", position, rotation);

    public GameObject CreateSafeContainer(Vector3 position) => 
        AssetProvider.InstantiateGameObjectAt("SafeContainer", position);
}

public interface IGameFactory
{
    Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation);
    Player CreatePlayer();
    GameGrid CreateGrid();
    Bomb CreateBomb(Vector3 position, Quaternion rotation);
    GameObject CreateSafeContainer(Vector3 position);
}

