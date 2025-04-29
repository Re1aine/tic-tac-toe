public class GameplaySceneProvider : IGameplaySceneProvider
{
    public GameField GameField { get; }

    public GameplaySceneProvider(GameField gameField)
    {
        GameField = gameField;
    }
}

public interface IGameplaySceneProvider
{
    GameField GameField { get; }
}