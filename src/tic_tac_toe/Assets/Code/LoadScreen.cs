using System;
using UnityEngine;

public class LoadScreen : MonoBehaviour, ILoadScreen
{
    public event Action Start;
    public event Action End;

    private readonly int ShowScreen = Animator.StringToHash("Show");
    private static int HideScreen = Animator.StringToHash("Hide");
    
    private Animator _animator;
    
    private void Awake() => _animator = GetComponent<Animator>();

    public void Show()
    {
        //Start?.Invoke();
    }

    public void Hide()
    {
        //End?.Invoke();
    }
}

public interface ILoadScreen
{
    void Show();
    void Hide();
}