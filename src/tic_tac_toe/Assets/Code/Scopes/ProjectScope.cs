using VContainer;
using VContainer.Unity;

public class ProjectScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<CoroutineRunner>().As<ICoroutineRunner>();
        //builder.RegisterComponentInHierarchy<LoadScreen>().As<ILoadScreen>();
        builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();

        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<GameStateMachine>(Lifetime.Singleton);


        builder.RegisterEntryPoint<ProjectEntryPoint>();
    }
}