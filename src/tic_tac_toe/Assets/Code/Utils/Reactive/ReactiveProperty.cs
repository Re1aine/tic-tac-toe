using System;
using System.Collections;

public class ReactiveProperty<T> : IReactiveProperty<T>
{
    public event Action<T> OnValueChanged;

    private T _value;

    public ReactiveProperty(T value)
    {
        _value = value;
    }
    
    public T Value
    {
        get => _value;
        
        private set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public void SetValue(T value) => Value = value;
}

