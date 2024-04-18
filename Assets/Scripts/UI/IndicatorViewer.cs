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

    protected abstract void SetStartValues(float value, float maxValue);

    protected abstract void Display();

    protected virtual void UpdateValue(float value, float maxValue)
    {
        TargetValue = (float)value / maxValue;

        Display();
    }
}