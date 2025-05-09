public interface IPauseService
{
    bool IsPaused { get; }
    void Add(IPausable pausable);
    void Remove(IPausable pausable);
    void Pause();
    void UnPause();
}