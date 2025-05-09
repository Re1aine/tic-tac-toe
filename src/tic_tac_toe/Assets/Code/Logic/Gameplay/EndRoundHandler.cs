using System;
using System.Collections;
using UnityEngine;

public class EndRoundHandler
{
    public event Action<RoundState> RoundEnd;
    
    private const int TimerToWin = 3;

    private readonly ICoroutineRunner _coroutineRunner;
    private readonly RoundStateTracker _roundStateTracker;

    private bool _isActive;
    
    public EndRoundHandler(ICoroutineRunner coroutineRunner, RoundStateTracker roundStateTracker)
    {
        _coroutineRunner = coroutineRunner;
        _roundStateTracker = roundStateTracker;
        
        _roundStateTracker.RoundStateChanged += StartTimerToWin;
    }
    
    private void StartTimerToWin()
    {
        if (_roundStateTracker.State != RoundState.InProgress && !_isActive) 
            _coroutineRunner.StartCoroutine(CountTimerToWin(TimerToWin), CoroutineScopes.Gameplay);
    }

    private IEnumerator CountTimerToWin(float duration)
    {
        _isActive = true;
        RoundState enterState = _roundStateTracker.State;

        while (duration >= 0)
        {
            if (enterState != _roundStateTracker.State)
            {
                _isActive = false;
                yield break;    
            }
            
            duration -= Time.deltaTime;
            Debug.Log($"Remain time to win - {duration}");
            yield return null;
        }

        _isActive = false;
        RoundEnd?.Invoke(_roundStateTracker.State);
    }
}