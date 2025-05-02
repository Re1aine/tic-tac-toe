using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayScope : LifetimeScope
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private Player _player;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInNewPrefab(_gameField, Lifetime.Singleton);
        builder.RegisterComponentInNewPrefab(_player, Lifetime.Singleton);
        
        builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();
        builder.Register<GameplayCameraService>(Lifetime.Singleton).As<ICameraService>();
        builder.Register<GameplaySceneProvider>(Lifetime.Singleton).As<IGameplaySceneProvider>()
            .WithParameter(resolver => resolver.Resolve<GameField>());
        
        
        builder.Register<PooledProjectileFactory>(Lifetime.Singleton).As<IProjectileFactory>();
        builder.Register<PooledFigureFactory>(Lifetime.Singleton).As<IFigureFactory>();
        
        builder.Register<GameGrid>(Lifetime.Singleton);
        builder.Register<RoundStateTracker>(Lifetime.Singleton);
        builder.Register<EndRoundHandler>(Lifetime.Singleton);
        builder.Register<RoundStateHandler>(Lifetime.Singleton);
        
        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<GameplayStateMachine>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }
}