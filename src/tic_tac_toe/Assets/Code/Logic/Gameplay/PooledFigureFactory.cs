using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PooledFigureFactory : IFigureFactory
{
    private const string ParentName = "Figures";
    private const string FiguresPrefabsPath = "Figures";
    
    private const int DefaultPoolCapacity = 10;
    private const int MaxPoolSize = 15;
    
    private readonly IObjectResolver _resolver;
    private readonly IFiguresHolder _figuresHolder;
    private readonly IPauseService _pauseService;
    private readonly IStaticDataService _staticDataService;
    
    private Transform _figuresParent;

    private Dictionary<FigureType, Figure> _figures;
    private Dictionary<FigureType, ObjectPool<Figure>> _dictionary;
    
    private PooledFigureFactory(IObjectResolver resolver, IFiguresHolder figuresHolder,
        IPauseService pauseService,
        IStaticDataService staticDataService)
    {
        _resolver = resolver;
        _figuresHolder = figuresHolder;
        _pauseService = pauseService;
        _staticDataService = staticDataService;
    }

    private Figure CreateNewFigure(FigureType figureType)
    {
        var prefab = _figures[figureType];
        var figure = _resolver.Instantiate(prefab, _figuresParent);
        figure.Destroyed += () => _dictionary[figureType].Release(figure);
        return figure;
    }

    public Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation)
    {
        var figure = _dictionary[type].Get();
        figure.transform.position = position;
        figure.transform.rotation = rotation;
        figure.SetLifeTime(_staticDataService.FigureStaticData.Lifetime);
        figure.ResetRigidbody();
        return figure;
    }

    public void Initialize()
    {
        _figures = Resources.LoadAll<Figure>(FiguresPrefabsPath).
            ToDictionary(x => x.FigureType, x => x);

        _dictionary = new Dictionary<FigureType, ObjectPool<Figure>>();
        
        _figuresParent = new GameObject(ParentName).transform;

        foreach (var figure in _figures)
        {
            var figuresPool = new ObjectPool<Figure>(
                () => CreateNewFigure(figure.Key),
                OnGetFigure,
                OnReleaseFigure,
                null, false, DefaultPoolCapacity, MaxPoolSize);
            
            _dictionary.Add(figure.Key, figuresPool);
            
            for (int i = 0; i < DefaultPoolCapacity; i++) 
                figuresPool.Release(CreateNewFigure(figure.Key));
        }
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
    Figure CreateFigure(FigureType type, Vector3 position, Quaternion rotation);
    void Initialize();
}