using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class RestartRoundState : IState
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IObjectResolver _resolver;

    public RestartRoundState(ICoroutineRunner coroutineRunner, IObjectResolver resolver)
    {
        _coroutineRunner = coroutineRunner;
        _resolver = resolver;
    }

    public void Enter()
    {
        var grid = _resolver.Resolve<GameGrid>();
        _coroutineRunner.StartCoroutine(WaveClearAnimation(grid), CoroutineScopes.Gameplay);
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
            foreach (var cell in currentLayerCells)
            {
                cell.Clear();
            }

            // Ждем перед следующим слоем
            yield return new WaitForSeconds(delayBetweenCells);
        }
    }
    
    public void Exit()
    {
    }
}