using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SmoothSliderView))]
public class SliderGetter : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private SmoothSliderView _sliderView;

    private void Start()
    {
        _sliderView = GetComponent<SmoothSliderView>();

        _sliderView.GetSlider(_slider);
    }
}