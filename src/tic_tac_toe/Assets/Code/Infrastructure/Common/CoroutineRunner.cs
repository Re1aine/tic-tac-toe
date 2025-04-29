using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    private readonly Dictionary<CoroutineScopes, List<Coroutine>> _coroutines = new()
    {
        { CoroutineScopes.Global, new List<Coroutine>()},
        { CoroutineScopes.Menu, new List<Coroutine>() },
        { CoroutineScopes.Gameplay, new List<Coroutine>() }
    };
    
    private void Awake() => DontDestroyOnLoad(this);
    
    public Coroutine StartCoroutine(IEnumerator coroutine, CoroutineScopes scope)
    {
        var wrappedCoroutine = StartCoroutine(WrapCoroutine(coroutine, scope));
        _coroutines[scope].Add(wrappedCoroutine);
        return wrappedCoroutine;
    }

    public void StopCoroutine(Coroutine coroutine, CoroutineScopes scope)
    {
        if (coroutine == null || !_coroutines.TryGetValue(scope, out var list)) 
            return;

        if (list.Remove(coroutine))
        {
            StopCoroutine(coroutine);
        }
    }

    public void StopCoroutines(CoroutineScopes scope)
    {
        if (!_coroutines.TryGetValue(scope, out var list)) 
            return;

        foreach (var cor in list.ToArray())
        {
            if (cor != null)
            {
                StopCoroutine(cor);
            }
        }
        list.Clear();
    }


    private IEnumerator WrapCoroutine(IEnumerator coroutine, CoroutineScopes scope)
    {
        yield return coroutine;
        RemoveCoroutine(scope);
    }
    
    private void RemoveCoroutine(CoroutineScopes scope)
    {
        if (_coroutines.TryGetValue(scope, out var list)) 
            list.RemoveAll(c => c == null);
    }
}

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator coroutine, CoroutineScopes scope);
    void StopCoroutine(Coroutine coroutine, CoroutineScopes scope);
    void StopCoroutines(CoroutineScopes scope);
}

public enum CoroutineScopes
{
    Global = 0,
    Menu = 1,
    Gameplay = 2,
}