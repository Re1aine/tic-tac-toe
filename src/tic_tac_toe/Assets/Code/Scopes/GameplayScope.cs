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
        
        builder.Register<UIFactory>(Lifetime.Singleton).As<IUIFactory>();
        builder.Register<HUDProvider>(Lifetime.Singleton).As<IHUDProvider>();
        
        builder.Register<PauseService>(Lifetime.Singleton).As<IPauseService>();
        
        builder.Register<PooledProjectileFactory>(Lifetime.Singleton).As<IProjectileFactory>();
        builder.Register<GameplayPooledFigureFactory>(Lifetime.Singleton).As<IFigureFactory>();
        
        builder.Register<ProjectilesHolder>(Lifetime.Singleton).As<IProjectilesHolder>();
        builder.Register<FiguresHolder>(Lifetime.Singleton).As<IFiguresHolder>();
        
        builder.Register<BombSpawner>(Lifetime.Singleton).As<IBombSpawner>();
        builder.Register<SafeContainerSpawner>(Lifetime.Singleton).As<ISafeContainerSpawner>();


        builder.Register<GameGrid>(Lifetime.Singleton);
        builder.Register<RoundStateTracker>(Lifetime.Singleton);
        builder.Register<EndRoundHandler>(Lifetime.Singleton);
        builder.Register<RoundStateHandler>(Lifetime.Singleton);
        
        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<GameplayStateMachine>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }
}