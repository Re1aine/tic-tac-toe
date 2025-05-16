using System.Collections.Generic;

public class PauseService : IPauseService
{
    public bool IsPaused => _isPaused;

    private bool _isPaused;
    private readonly List<IPausable> _pausables = new();
    
    public void Add(IPausable pausable) => _pausables.Add(pausable);
    public void Remove(IPausable pausable) => _pausables.Remove(pausable);
    
    public void Pause()
    {
        _isPaused = true;
        _pausables.ForEach(p => p.Pause());
    }

    public void UnPause()
    {
        _isPaused = false;
        _pausables.ForEach(p => p.UnPause());
    }
}

public interface IPausable
{
    void Pause();
    void UnPause();
}