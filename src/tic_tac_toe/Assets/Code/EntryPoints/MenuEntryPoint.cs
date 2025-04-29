using VContainer.Unity;

public class MenuEntryPoint : IInitializable
{
    private readonly MenuStateMachine _gameStateMachine;

    public MenuEntryPoint(MenuStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
        _gameStateMachine.Enter<MenuState>();
    }
}