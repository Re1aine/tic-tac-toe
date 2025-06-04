using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameplayUI : MonoBehaviour
{
    [field: SerializeField] public Transform Root;

    [SerializeField] private Button _pause;
    [SerializeField] private Timer _timer;
    [SerializeField] private Button _bomb;
    
    private GameplayStateMachine _gameplayStateMachine;
    private IAbilityReleaseService _abilityReleaseService;


    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine, IAbilityReleaseService abilityReleaseService)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _abilityReleaseService = abilityReleaseService;
    }
    
    private void Start()
    {
        _pause.onClick.AddListener(() => _gameplayStateMachine.Enter<PauseRoundState>());
        _bomb.onClick.AddListener(() => _abilityReleaseService.ReleaseAbility(AbilityId.Bomb));
        _timer.ResetTimer();
    }

    public void ResetTimer() => _timer.ResetTimer();
}