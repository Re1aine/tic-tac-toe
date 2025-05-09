
public class EndRoundState : IStateWithArg<RoundState>
{
    private readonly IUIFactory _uiFactory;

    private ResultWindow _resultWindow;
    public EndRoundState(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void Enter(RoundState result)
    {
        _resultWindow = _uiFactory.CreateResultWindow();
    }

    public void Exit()
    {
        _resultWindow.Destroy();
    }
}