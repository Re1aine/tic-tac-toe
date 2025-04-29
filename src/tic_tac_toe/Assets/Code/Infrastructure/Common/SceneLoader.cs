using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;
    public SceneLoader(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

    public void LoadScene(string scene, Action onLoaded = null) => 
        _coroutineRunner.StartCoroutine(LoadSceneAsync(scene, onLoaded), CoroutineScopes.Global);

    private IEnumerator LoadSceneAsync(string scene, Action isLoaded)
    {
        if (SceneManager.GetActiveScene().name == scene)
        {
            isLoaded?.Invoke();
            yield break;
        }
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        yield return new WaitUntil(() => asyncOperation.isDone);
        
        isLoaded?.Invoke();
    }
}

public interface ISceneLoader
{
    void LoadScene(string scene, Action onLoaded = null);
}

public enum GameScenes
{
    Entry = 0,
    Menu = 1,
    Gameplay = 2
}