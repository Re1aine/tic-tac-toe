using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button _toMenu;
    private GameplayStateMachine _gameStateMachine;
    
    [Inject]
    public void Construct(GameplayStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;        
    }
    
    private void Start()
    {
        _toMenu.onClick.AddListener(() => _gameStateMachine.Enter<ExitGameplayState>());
    }
}