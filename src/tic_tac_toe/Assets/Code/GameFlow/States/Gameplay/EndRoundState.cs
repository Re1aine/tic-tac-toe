
public class EndRoundState : IStateWithArg<RoundState>
{
    private readonly IUIFactory _uiFactory;
    private readonly IPauseService _pauseService;

    private ResultWindow _resultWindow;
    public EndRoundState(IUIFactory uiFactory, IPauseService pauseService)
    {
        _uiFactory = uiFactory;
        _pauseService = pauseService;
    }

    public void Enter(RoundState result)
    {
        _resultWindow = _uiFactory.CreateResultWindow();
        _pauseService.Pause();
    }

    public void Exit()
    {
        _resultWindow.Destroy();
    }
}