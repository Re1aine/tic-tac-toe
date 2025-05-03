using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameplayLoopState : IState
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IGameFactory _gameFactory;
    private readonly IGameplaySceneProvider _gameplaySceneProvider;
    private readonly IObjectResolver _resolver;

    public GameplayLoopState(ICoroutineRunner coroutineRunner, IGameFactory gameFactory,
        IGameplaySceneProvider gameplaySceneProvider,
        IObjectResolver resolver)
    {
        _coroutineRunner = coroutineRunner;
        _gameFactory = gameFactory;
        _gameplaySceneProvider = gameplaySceneProvider;
        _resolver = resolver;
    }

    public void Enter()
    {
        var grid = _resolver.Resolve<GameGrid>();
        _coroutineRunner.StartCoroutine(SpawnBombs(), CoroutineScopes.Gameplay);
        _coroutineRunner.StartCoroutine(SpawnSafeContainers(grid), CoroutineScopes.Gameplay);
        _coroutineRunner.StartCoroutine(SpawnWaveClear(grid), CoroutineScopes.Gameplay);
    }

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            for (int i = 0; i < 1; i++)
            {
                _gameFactory.CreateBomb(
                    RandomHelper.GetRandomPositionInCollider(_gameplaySceneProvider.GameField.SpawnZone),
                    RandomHelper.GetRandomRotation());
            }
            yield return new WaitForSeconds(5);
        }
    }
    private IEnumerator SpawnSafeContainers(GameGrid grid)
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            
            // 1. Выбираем случайную клетку
            int x = Random.Range(0, grid.Grid.GetLength(0));
            int y = Random.Range(0, grid.Grid.GetLength(1));
            var targetCell = grid.Grid[x, y];
            
            // 2. Создаем контейнер ниже клетки
            Vector3 spawnPosition = targetCell.transform.position + Vector3.down * 5f;
            var container = _gameFactory.CreateSafeContainer(spawnPosition);
            
            // 3. Поднимаем контейнер
            Vector3 targetPosition = targetCell.transform.position + Vector3.up * 2;
            yield return MoveObject(container.transform, spawnPosition, targetPosition, 5);
            
            // 4. Ждем
            yield return new WaitForSeconds(5);
            
            // 5. Опускаем контейнер
            yield return _coroutineRunner.StartCoroutine(MoveObject(container.transform, targetPosition, spawnPosition, 5),
                CoroutineScopes.Gameplay);
            
            // 6. Уничтожаем
            Object.Destroy(container);
        }
    }
    private IEnumerator MoveObject(Transform obj, Vector3 from, Vector3 to, float speed)
    {
        float journeyLength = Vector3.Distance(from, to);
        float startTime = Time.time;
        
        while (true)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            
            obj.position = Vector3.Lerp(from, to, fractionOfJourney);
            
            if (fractionOfJourney >= 1f) break;
            yield return null;
        }
    }
    private IEnumerator SpawnWaveClear(GameGrid grid)
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            yield return _coroutineRunner.StartCoroutine(WaveClearAnimation(grid), CoroutineScopes.Gameplay);
        }
    }
    private IEnumerator WaveClearAnimation(GameGrid grid)
    {
        int size = grid.Grid.GetLength(0); // Предполагаем квадратную сетку
        float delayBetweenCells = 0.3f;

        // Проходим по всем диагональным слоям
        for (int layer = 0; layer < size * 2 - 1; layer++)
        {
            // Определяем стартовые позиции для текущего слоя
            int startRow = Mathf.Min(layer, size - 1);
            int startCol = Mathf.Max(0, layer - size + 1);

            // Собираем все клетки в текущем диагональном слое
            List<Cell> currentLayerCells = new List<Cell>();
            for (int i = 0; i <= Mathf.Min(startRow, size - 1 - startCol); i++)
            {
                int row = startRow - i;
                int col = startCol + i;
                currentLayerCells.Add(grid.Grid[row, col]);
            }

            // Запускаем анимацию для всех клеток слоя одновременно

            for (int i = 0; i  < currentLayerCells.Count; i++)
            {
                currentLayerCells[i].Clear();
            }
            //foreach (var cell in currentLayerCells)
            //{
            //    cell.Clear();
            //}

            // Ждем перед следующим слоем
            yield return new WaitForSeconds(delayBetweenCells);
        }
    }

    public void Exit()
    {
        
    }
}