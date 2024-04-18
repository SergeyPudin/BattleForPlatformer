using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderView : IndicatorViewer
{
    [SerializeField] private float _slideDuration;

    private Slider _slider;
    private Coroutine _moveCoroutine;
    private float _previusValue;
   
    public void GetSlider(Slider slider)
    {
        _slider = slider;
    }

    protected override void SetStartValues(float value, float maxValue)
    {
        _slider.value = (float)value / maxValue;

        _previusValue = _slider.value;
    }

    protected override void Display()
    {
        _moveCoroutine = StartCoroutine(SmoothSlide());
    }

    private IEnumerator SmoothSlide()
    {
        float elapsed = 0;

        while (elapsed < _slideDuration)
        {
            _slider.value = Mathf.MoveTowards(_previusValue, TargetValue, elapsed / _slideDuration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        _previusValue = TargetValue;
        _moveCoroutine = null;
    }
}