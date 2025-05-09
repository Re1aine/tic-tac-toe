
public class GameplayLoopState : IState
{
    private readonly IBombSpawner _bombSpawner;
    private readonly ISafeContainerSpawner _safeContainerSpawner;

    public GameplayLoopState(IBombSpawner bombSpawner, ISafeContainerSpawner safeContainerSpawner)
    {
        _bombSpawner = bombSpawner;
        _safeContainerSpawner = safeContainerSpawner;
    }

    public void Enter()
    {
        _bombSpawner.Enable();
        _safeContainerSpawner.Enable();
    }
    
    public void Exit()
    {
        
    }
}