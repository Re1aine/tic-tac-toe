using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MenuScope : LifetimeScope
{
    [SerializeField] private Collider _spawnZone;
    [SerializeField] private Camera _staticCamera;
    [SerializeField] private Camera _orbitCamera;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();
        builder.Register<MenuCameraService>(Lifetime.Singleton).As<ICameraService>();
        builder.Register<MenuSceneProvider>(Lifetime.Singleton).As<IMenuSceneProvider>()
            .WithParameter(_spawnZone)
            .WithParameter("staticCamera", _staticCamera)
            .WithParameter("orbitCamera", _orbitCamera);


        builder.Register<MenuPooledFigureFactory>(Lifetime.Singleton).As<IFigureFactory>();
        
        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<MenuStateMachine>(Lifetime.Singleton);
        
        
        builder.RegisterEntryPoint<MenuEntryPoint>();
    }
}

