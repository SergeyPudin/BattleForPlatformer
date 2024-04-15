using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderView : AbstractIndicatorViewer
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _slideDuration;

    private Coroutine _moveCoroutine;
    private float _previusValue;

    public void GetSlider(Slider slider)
    {
        _slider = slider;
    }

    protected override void SetStartValues(int value, int maxValue)
    {
        MaxValue = maxValue;
        _previusValue = value;

        _slider.maxValue = maxValue;
        _slider.value = value;
    }

    protected override void Display()
    {
        _slider.maxValue = MaxValue;

        _moveCoroutine = StartCoroutine(SmoothSlide());
    }

    private IEnumerator SmoothSlide()
    {
        float elapsed = 0;

        while (elapsed < _slideDuration)
        {
            _slider.value = Mathf.MoveTowards(_previusValue, CurrentValue, elapsed / _slideDuration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        _previusValue = CurrentValue;
        _moveCoroutine = null;
    }
}