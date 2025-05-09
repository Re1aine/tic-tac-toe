using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _exit;
    
    private GameplayStateMachine _gameplayStateMachine;

    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine)
    {
        _gameplayStateMachine = gameplayStateMachine;
    }

    private void Start()
    {
        _continue.onClick.AddListener(() => _gameplayStateMachine.Enter<ResumeRoundState>());
        _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<RestartRoundState>());
        _exit.onClick.AddListener(() => _gameplayStateMachine.Enter<ExitGameplayState>());
        
    }

    public void Destroy() => Destroy(gameObject);
}