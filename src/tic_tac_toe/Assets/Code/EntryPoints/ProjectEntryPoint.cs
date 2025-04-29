using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectEntryPoint : IInitializable
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IObjectResolver _resolver;

    public ProjectEntryPoint(GameStateMachine gameStateMachine, IObjectResolver resolver)
    {
        _gameStateMachine = gameStateMachine;
        _resolver = resolver;
    }
    
    public void Initialize()
    {
        //_gameStateMachine.AddState(_resolver.Resolve<InitializeState>());
        //_gameStateMachine.AddState(_resolver.Resolve<LoadSceneState>());
        
        _gameStateMachine.Enter<InitializeState>();
    }
}
