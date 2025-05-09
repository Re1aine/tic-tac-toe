using UnityEngine;

public class WaitForSecondsUnpaused : CustomYieldInstruction
{
    private readonly IPauseService _pauseService;
    private float _seconds;

    public WaitForSecondsUnpaused(IPauseService pauseService, float seconds)
    {
        _pauseService = pauseService;
        _seconds = seconds;
    }
    
    public override bool keepWaiting
    {
        get
        {
            if (_pauseService.IsPaused)
                return true;
            
            _seconds -= Time.deltaTime;
            return _seconds > 0f;
        }
    }
}