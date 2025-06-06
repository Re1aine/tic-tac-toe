using System.Collections.Generic;

public class AbilityReleaseService : IAbilityReleaseService
{
    private readonly IGameplayFactory _gameplayFactory;
    private readonly IGameplaySceneProvider _gameplaySceneProvider;
    private readonly ICoroutineRunner _coroutineRunner;

    private readonly Dictionary<AbilityId, Ability> _abilities;

    public AbilityReleaseService(IGameplayFactory gameplayFactory, IGameplaySceneProvider gameplaySceneProvider, ICoroutineRunner coroutineRunner)
    {
        _gameplayFactory = gameplayFactory;
        _gameplaySceneProvider = gameplaySceneProvider;
        _coroutineRunner = coroutineRunner;

        _abilities = new Dictionary<AbilityId, Ability>()
        {
            [AbilityId.Bomb] = new BombAbility(_gameplayFactory, _gameplaySceneProvider, _coroutineRunner)
        };
    }

    public void ReleaseAbility(AbilityId id) => _abilities[id].Use();
}

public interface IAbilityReleaseService
{
    void ReleaseAbility(AbilityId id);
}