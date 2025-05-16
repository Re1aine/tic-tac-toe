using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _play;
    
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

    private void Start() => _play.onClick.AddListener(() => _gameStateMachine.Enter<StartGameplayState>());

    private void Update()
    {
        _play.transform.position = new Vector3(_play.transform.position.x,
            _play.transform.position.y + Mathf.Sin(Time.time * 2f) * 0.5f,
            _play.transform.position.z);
    }
}