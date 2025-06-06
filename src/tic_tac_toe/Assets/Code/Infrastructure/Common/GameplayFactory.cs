using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayFactory : GameFactory, IGameplayFactory
{
    private readonly IProjectileFactory _projectileFactory;
    private readonly ISafeContainersHolder _safeContainersHolder;
    private readonly IBombsHolder _bombsHolder;

    public GameplayFactory(IObjectResolver objectResolver,
        IFigureFactory figureFactory,
        IProjectileFactory projectileFactory,
        ISafeContainersHolder safeContainersHolder,
        IBombsHolder bombsHolder)
        : base(objectResolver, figureFactory)
    {
        _projectileFactory = projectileFactory;
        _safeContainersHolder = safeContainersHolder;
        _bombsHolder = bombsHolder;
    }

    public GameGrid CreateGrid()
    {
        var grid = _objectResolver.Resolve<GameGrid>();
        grid.Initialize();
        return grid;
    }

    public Bomb CreateBomb(Vector3 position, Quaternion rotation)
    {
        var prefab = Resources.Load<Bomb>("Bomb");
        var bomb = _objectResolver.Instantiate(prefab, position, rotation);
        
        _bombsHolder.Add(bomb);
        bomb.Destroyed += _bombsHolder.Remove;
        
        return bomb;
    }

    public Player CreatePlayer()
    {
        var prefab = Resources.Load<Player>("Player");
        return _objectResolver.Instantiate(prefab);
    }

    public SafeContainer CreateSafeContainer(Vector3 position)
    {
        var prefab = Resources.Load<SafeContainer>("SafeContainer");
        var safeContainer =  _objectResolver.Instantiate(prefab, position, Quaternion.identity);
        
        _safeContainersHolder.Add(safeContainer);
        safeContainer.Destroyed += _safeContainersHolder.Remove;
        
        return safeContainer;
    }

    public Projectile CreateProjectile(Vector3 position, Quaternion rotation) => 
        _projectileFactory.CreateProjectile(position, rotation);

    public SimpleBot CreateSimpleBot()
    {
        var prefab =  Resources.Load<SimpleBot>("SimpleBot");
        return _objectResolver.Instantiate(prefab);
    }
}

public interface IGameplayFactory : IGameFactory
{
    Player CreatePlayer();
    GameGrid CreateGrid();
    Bomb CreateBomb(Vector3 position, Quaternion rotation);
    SafeContainer CreateSafeContainer(Vector3 position);
    Projectile CreateProjectile(Vector3 position, Quaternion rotation);
    SimpleBot CreateSimpleBot();
}