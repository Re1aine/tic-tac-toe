using System.Collections.Generic;

public class PauseService : IPauseService
{
    public bool IsPaused => _isPaused;

    private bool _isPaused;
    private readonly List<IPausable> _pausables = new();
    private readonly IProjectilesHolder _projectilesHolder;
    private readonly IFiguresHolder _figuresHolder;
    
    public PauseService(IProjectilesHolder projectilesHolder, IFiguresHolder figuresHolder)
    {
        _projectilesHolder = projectilesHolder;
        _figuresHolder = figuresHolder;
    }

    public void Add(IPausable pausable) => _pausables.Add(pausable);
    public void Remove(IPausable pausable) => _pausables.Remove(pausable);
    
    public void Pause()
    {
        _isPaused = true;
        
        foreach (var projectile in _projectilesHolder.Projectiles) 
            projectile.Pause();
        
        foreach (var figure in _figuresHolder.Figures)
            figure.Pause();

        _pausables.ForEach(p => p.Pause());
    }

    public void UnPause()
    {
        _isPaused = false;
        
        foreach (var projectile in _projectilesHolder.Projectiles) 
            projectile.UnPause();
        
        foreach (var figure in _figuresHolder.Figures)
            figure.UnPause();

        _pausables.ForEach(p => p.UnPause());
    }
}

public interface IPausable
{
    void Pause();
    void UnPause();
}