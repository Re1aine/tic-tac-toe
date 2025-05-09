using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public abstract class PooledFigureFactory : IFigureFactory
{
    private const string ParentName = "Figures";
    
    private readonly IObjectResolver _resolver;

    private ObjectPool<Figure> _figuresPool;

    private Figure _figurePrefab;
    private Transform _figuresParent;
    
    private readonly float _lifeTimeFigure = 15f;

    protected PooledFigureFactory(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    private Figure CreateNewFigure()
    {
        var figure = _resolver.Instantiate(_figurePrefab, _figuresParent);
        figure.Destroyed += () => _figuresPool.Release(figure);
        return figure;
    }

    public virtual Figure CreateFigure(Vector3 position, Quaternion rotation)
    {
        var figure = _figuresPool.Get();
        figure.transform.position = position;
        figure.transform.rotation = rotation;
        figure.SetLifeTime(_lifeTimeFigure);
        figure.ResetRigidbody();
        return figure;
    }

    public void Initialize()
    {
        _figuresPool = new ObjectPool<Figure>(CreateNewFigure, OnGetFigure, OnReleaseFigure,
            null, false, 5, 7);

        _figurePrefab = Resources.Load<Figure>("Cross");
        _figuresParent = new GameObject(ParentName).transform;
        
        for (int i = 0; i < 5; i++) _figuresPool.Release(CreateNewFigure());
    }

    private void OnGetFigure(Figure figure) => 
        figure.gameObject.SetActive(true);

    private void OnReleaseFigure(Figure figure) => 
        figure.gameObject.SetActive(false);
}

public interface IFigureFactory
{
    Figure CreateFigure(Vector3 position, Quaternion rotation);
    void Initialize();
}