using TMPro;
using UnityEngine;
using VContainer;

public class Timer : MonoBehaviour, IPausable
{
    private const string DefaultValue = "00:00";

    [SerializeField] private TextMeshProUGUI _timerText;
    
    private GameplayStateMachine _gameplayStateMachine;
    private IStaticDataService _staticDataService;
    private IPauseService _pauseService;

    private float _currentTime;
    private bool _isPaused;
    private bool _timerEnded;

    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine, IPauseService pauseService, IStaticDataService staticDataService)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _pauseService = pauseService;
        _staticDataService = staticDataService;
    }
    
    private void Start()
    {
        _pauseService.Add(this);
        ResetTimer();
    }

    private void Update()
    {
        if (_isPaused || _timerEnded) return;
        
        if (_currentTime <= 0)
        {
            EndTimer();
            return;
        }
        _currentTime -= Time.deltaTime;
        UpdateTimerDisplay();
    }

    private void EndTimer()
    {
        _timerText.text = DefaultValue;
        _timerText.color = _staticDataService.TimerStaticData.DangerColor;
        _timerEnded = true;
        OnTimerEnd();
    }

    public void ResetTimer()
    {
        _timerEnded = false;
        _currentTime = _staticDataService.TimerStaticData.RoundDuration;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";

        if (_currentTime <= _staticDataService.TimerStaticData.DangerThreshold)
            _timerText.color = _staticDataService.TimerStaticData.DangerColor;
        else if (_currentTime <= _staticDataService.TimerStaticData.WarningThreshold)
            _timerText.color = _staticDataService.TimerStaticData.WarningColor;
        else
            _timerText.color = _staticDataService.TimerStaticData.NormalColor;
    }

    private void OnTimerEnd() => _gameplayStateMachine.Enter<EndRoundState, RoundState>(RoundState.CircleWin);
    private void OnDestroy() => _pauseService.Remove(this);
    public void Pause() => _isPaused = true;
    public void UnPause() => _isPaused = false;
}