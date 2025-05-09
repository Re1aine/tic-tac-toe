using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameplayUI : MonoBehaviour
{
    [field: SerializeField] public Transform Root;

    [SerializeField] private Button _pause;
    [SerializeField] private Timer _timer;
    
    private GameplayStateMachine _gameplayStateMachine;

    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine)
    {
        _gameplayStateMachine = gameplayStateMachine;
    }
    
    private void Start()
    {
        _pause.onClick.AddListener(() => _gameplayStateMachine.Enter<PauseRoundState>());
        _timer.ResetTimer();
    }

    public void ResetTimer() => _timer.ResetTimer();
}
