using System.Collections;
using UnityEngine;

public class BombAbility : Ability
{
    private const int BombCount = 4;
    private const int CooldownTime = 10;
    
    private readonly IGameplayFactory _gameplayFactory;
    private readonly IGameplaySceneProvider _gameplaySceneProvider;
    private readonly ICoroutineRunner _coroutineRunner;

    public BombAbility(IGameplayFactory gameplayFactory, IGameplaySceneProvider gameplaySceneProvider, ICoroutineRunner coroutineRunner)
    {
        _gameplayFactory = gameplayFactory;
        _gameplaySceneProvider = gameplaySceneProvider;
        _coroutineRunner = coroutineRunner;
    }
    
    public override void Use()
    {
        base.Use();
        
        if (!IsCanUse) return;
        _coroutineRunner.StartCoroutine(StartTimer(), CoroutineScopes.Gameplay);
        CreateBombsAtCorners();
    }

    private IEnumerator StartTimer()
    {
        IsCanUse = false;
        Cooldown = CooldownTime;
        
        while (Cooldown >= 0)
        {
            Cooldown -= Time.deltaTime;
            yield return null;
        } 
        
        IsCanUse = true;
    }

    private void CreateBombsAtCorners()
    {
        Bounds bounds = _gameplaySceneProvider.GameField.SpawnZone.bounds;
        var offset = 1.5f;
    
        for (int i = 0; i < BombCount; i++)
        {
            Vector3 corner = new Vector3(
                (i % 2 == 0) ? bounds.min.x : bounds.max.x,
                bounds.min.y,
                (i < 2) ? bounds.min.z : bounds.max.z
            );
            
            _gameplayFactory.CreateBomb(corner / offset, RandomHelper.GetRandomRotation());
        }
    }
}