using UnityEngine;

[RequireComponent(typeof(IChangeValue))]
public abstract class IndicatorViewer : MonoBehaviour
{
    protected int MaxValue;
    protected float TargetValue;
    protected IChangeValue ChangeValue;

    private void Awake()
    {
        ChangeValue = GetComponent<IChangeValue>();
    }

    private void OnEnable()
    {
        ChangeValue.OnValueChanged += UpdateValue;
        ChangeValue.Reset += SetStartValues;
    }

    private void OnDisable()
    {
        ChangeValue.OnValueChanged -= UpdateValue;
        ChangeValue.Reset += SetStartValues;
    }

    protected abstract void SetStartValues(int value, int maxValue);

    protected abstract void Display();

    protected virtual void UpdateValue(int value, int maxValue)
    {
        TargetValue = (float)value / maxValue;

        Display();
    }
}