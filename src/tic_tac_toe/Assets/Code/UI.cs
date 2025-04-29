using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _quit;
    
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;        
    }

    private void Start()
    {
        _play.onClick.AddListener(() => _gameStateMachine.Enter<StartGameplayState>());
        _quit.onClick.AddListener(Application.Quit);
    }
}