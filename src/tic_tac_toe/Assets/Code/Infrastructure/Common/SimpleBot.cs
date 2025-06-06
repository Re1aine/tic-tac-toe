using System.Collections;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class SimpleBot : MonoBehaviour, ISimpleBot
{
    private IGameFactory _gameFactory;
    private IGameplaySceneProvider _gameplaySceneProvider;
    private ICoroutineRunner _coroutineRunner;
    private IPauseService _pauseService;
    private GameGrid _grid;
    
    private Coroutine _coroutine;

    [Inject]
    public void Construct(IGameFactory gameFactory,
        IGameplaySceneProvider gameplaySceneProvider,
        ICoroutineRunner coroutineRunner,
        IPauseService pauseService,
        GameGrid grid)
    {
        _gameFactory = gameFactory;
        _gameplaySceneProvider = gameplaySceneProvider;
        _coroutineRunner = coroutineRunner;
        _pauseService = pauseService;
        _grid = grid;
        
    }

    private void Start() => Activate();

    public void Activate() => _coroutine = _coroutineRunner.StartCoroutine(StartReleaseFigure(), CoroutineScopes.Gameplay);

    public void DisActivate() => _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

    public void ReleaseFigure(Vector3 position)
    {
        var pos = new Vector3(position.x,
            _gameplaySceneProvider.GameField.SpawnZone.transform.position.y,
            position.z);
        _gameFactory.CreateFigure(FigureType.Circle, pos, RandomHelper.GetRandomRotation());
    }

    private IEnumerator StartReleaseFigure()
    {
        while (true)
        {
           yield return new WaitUntil(() => !_pauseService.IsPaused);
           
           yield return new WaitForSecondsUnpaused(_pauseService,Random.Range(0f, 1.5f));
           var line = _grid.GetRandomLine();
           foreach (var cell in line)
           {
               yield return new WaitForSecondsUnpaused(_pauseService,Random.Range(0.5f, 1.2f));
               for (int i = 0; i < Random.Range(1, 7); i++)
               {
                   yield return new WaitForSecondsUnpaused(_pauseService, Random.Range(0.2f, 0.5f));
                   ReleaseFigure(cell.transform.position);
               }
               
               yield return new WaitForSecondsUnpaused(_pauseService, Random.Range(0.2f, 0.5f));
               if(Random.value > 0.5f)
                   ReleaseFigure(_grid.GetRandomCell().transform.position);
           } 
        }
    }
}

public interface ISimpleBot
{
    void Activate();
    void DisActivate();
    void ReleaseFigure(Vector3 position);
}   