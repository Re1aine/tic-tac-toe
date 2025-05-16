using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PooledFigureFactory : IFigureFactory
{
    private const string ParentName = "Figures";
    
    private readonly IObjectResolver _resolver;
    private readonly IFiguresHolder _figuresHolder;
    private readonly IPauseService _pauseService;
    private readonly IStaticDataService _staticDataService;

    private ObjectPool<Figure> _figuresPool;

    private Figure _figurePrefab;
    private Transform _figuresParent;
    
    protected PooledFigureFactory(IObjectResolver resolver, IFiguresHolder figuresHolder,
        IPauseService pauseService,
        IStaticDataService staticDataService)
    {
        _resolver = resolver;
        _figuresHolder = figuresHolder;
        _pauseService = pauseService;
        _staticDataService = staticDataService;
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
        figure.SetLifeTime(_staticDataService.FigureStaticData.Lifetime);
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

    private void OnGetFigure(Figure figure)
    {
        _figuresHolder.Add(figure);
        _pauseService.Add(figure);
        figure.gameObject.SetActive(true);
    }

    private void OnReleaseFigure(Figure figure)
    {
        _figuresHolder.Remove(figure);
        _pauseService.Remove(figure);
        figure.gameObject.SetActive(false);
    }
}

public interface IFigureFactory
{
    Figure CreateFigure(Vector3 position, Quaternion rotation);
    void Initialize();
}