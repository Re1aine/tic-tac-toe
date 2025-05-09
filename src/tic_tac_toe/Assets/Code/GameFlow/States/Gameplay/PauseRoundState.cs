
public class PauseRoundState : IState
{
    private readonly IUIFactory _uiFactory;
    private readonly IPauseService _pauseService;
    private readonly IProjectilesHolder _projectilesHolder;

    private PauseWindow _pauseWindow;
    public PauseRoundState(IUIFactory uiFactory, IPauseService pauseService)
    {
        _uiFactory = uiFactory;
        _pauseService = pauseService;
    }

    public void Enter()
    {
        _pauseService.Pause();
        _pauseWindow = _uiFactory.CreatePauseWindow();
    }

    public void Exit()
    {
        _pauseWindow.Destroy();
    }
}