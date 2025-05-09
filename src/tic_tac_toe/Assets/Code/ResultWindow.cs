using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private Button _toMainMenu;
    [SerializeField] private Button _restart;
    [SerializeField] private TextMeshProUGUI _resultText; 
        
    private GameplayStateMachine _gameplayStateMachine;
    
    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine)
    {
        _gameplayStateMachine = gameplayStateMachine;
    }

    private void Start()
    {
        _toMainMenu.onClick.AddListener(() => _gameplayStateMachine.Enter<ExitGameplayState>());
        _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<RestartRoundState>());
    }

    public void SetResultText() => _resultText.text = "??????? Win";

    public void Destroy() => Destroy(gameObject);
}