using TMPro;
using UnityEngine;
using VContainer;

public class Timer : MonoBehaviour, IPausable
{
    private const string DefaultValue = "00:00";

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _warningColor = Color.yellow;
    [SerializeField] private Color _dangerColor = Color.red;
    [SerializeField] private float _warningThreshold;
    [SerializeField] private float _dangerThreshold;
    [SerializeField] private float _roundDuration;
    
    private GameplayStateMachine _gameplayStateMachine;
    private IPauseService _pauseService;
    
    private float _currentTime;
    private bool _isPaused;
    private bool _timerEnded;

    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine, IPauseService pauseService)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _pauseService = pauseService;
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
        _timerText.color = _dangerColor;
        _timerEnded = true;
        OnTimerEnd();
    }

    public void ResetTimer()
    {
        _timerEnded = false;
        _currentTime = _roundDuration;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";

        if (_currentTime <= _dangerThreshold)
            _timerText.color = _dangerColor;
        else if (_currentTime <= _warningThreshold)
            _timerText.color = _warningColor;
        else
            _timerText.color = _normalColor;
    }

    private void OnTimerEnd() => _gameplayStateMachine.Enter<EndRoundState, RoundState>(RoundState.CircleWin);
    private void OnDestroy() => _pauseService.Remove(this);
    public void Pause() => _isPaused = true;
    public void UnPause() => _isPaused = false;
}