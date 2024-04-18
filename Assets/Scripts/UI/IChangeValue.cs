using UnityEngine.Events;

public interface IChangeValue
{
    event UnityAction<float, float> OnValueChanged;
    event UnityAction<float, float> Reset;
}