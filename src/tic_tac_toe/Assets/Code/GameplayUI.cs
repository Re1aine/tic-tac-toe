using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button _toMenu;
    [SerializeField] private Button _reStart;

    private GameplayStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameplayStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;        
    }

    private void Start()
    {
        _toMenu.onClick.AddListener(() => _gameStateMachine.Enter<ExitGameplayState>());
        _reStart.onClick.AddListener(() => _gameStateMachine.Enter<RestartRoundState>());
        
        StartTimer();
    }

    private void StartTimer()
    {
        _currentTime = _roundDuration;
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        _timerCoroutine = StartCoroutine(TimerCountdown());
    }

    private IEnumerator TimerCountdown()
    {
        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }
        
        // Таймер завершился
        _timerText.text = "00:00";
        _timerText.color = _dangerColor;
        OnTimerEnd();
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

    private void OnTimerEnd()
    {
        // Вызываем завершение раунда
        //_gameStateMachine.Enter<EndRoundState>();
    }

    private void OnDestroy()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
    }


    [Header("Timer Settings")]
    [SerializeField] private float _roundDuration = 300f; // 5 минут в секундах


    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _warningColor = Color.yellow;
    [SerializeField] private Color _dangerColor = Color.red;
    [SerializeField] private float _warningThreshold = 60f; // 1 минута
    [SerializeField] private float _dangerThreshold = 10f; // 10 секунд

    [SerializeField] private TextMeshProUGUI _timerText;
    
    private Coroutine _timerCoroutine;
    private float _currentTime;

}