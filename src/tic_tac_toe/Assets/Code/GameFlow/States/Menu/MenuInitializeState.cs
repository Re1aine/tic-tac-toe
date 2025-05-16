public class MenuInitializeState : IState
{
    private readonly MenuStateMachine _menuStateMachine;
    private readonly IFigureFactory _figureFactory;

    public MenuInitializeState(MenuStateMachine menuStateMachine, IFigureFactory figureFactory)
    {
        _menuStateMachine = menuStateMachine;
        _figureFactory = figureFactory;
    }

    public void Enter()
    {
        _figureFactory.Initialize();
        _menuStateMachine.Enter<MenuState>();
    }

    public void Exit()
    {
        
    }
}