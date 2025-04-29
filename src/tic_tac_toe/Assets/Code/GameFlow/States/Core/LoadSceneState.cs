using UnityEngine;

public class LoadSceneState : IStateWithArg<GameScenes>
{
    private readonly ISceneLoader _sceneLoader;

    public LoadSceneState(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Enter(GameScenes scene)
    {
        _sceneLoader.LoadScene(scene.ToString(), () => Debug.Log($"{scene} scene is loaded"));
    }

    public void Exit()
    {
        
    } 
}


