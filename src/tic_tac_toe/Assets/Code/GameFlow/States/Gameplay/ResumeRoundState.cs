public class ResumeRoundState : IState
{
    private readonly IPauseService _pauseService;

    public ResumeRoundState(IPauseService pauseService) => 
        _pauseService = pauseService;

    public void Enter() => _pauseService.UnPause();

    public void Exit()
    {
        
    }
}