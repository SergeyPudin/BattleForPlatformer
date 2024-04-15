using UnityEngine.Events;

public interface IChangeValue
{
    event UnityAction<int, int> OnValueChanged;
    event UnityAction<int, int> Reset;
}