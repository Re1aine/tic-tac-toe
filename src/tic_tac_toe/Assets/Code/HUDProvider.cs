public class HUDProvider : IHUDProvider
{
    private readonly IUIFactory _uiFactory;
    public GameplayUI GameplayUI => _gameplayUI;

    private GameplayUI _gameplayUI;
    
    public HUDProvider(IUIFactory uiFactory) => 
        _uiFactory = uiFactory;

    public void Initialize() => 
        _gameplayUI = _uiFactory.CreateHud();
}

public interface IHUDProvider
{
    GameplayUI GameplayUI { get; }
    void Initialize();
}