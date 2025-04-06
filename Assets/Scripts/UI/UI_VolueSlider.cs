using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolueSlider : MonoBehaviour
{
    public Slider slider;
    public string parametr;

    public AudioMixer audiomiexr;


    public void SliderValue(float _value)
    {
        audiomiexr.SetFloat(parametr, _value);
    }

    public void LoadSlider(float _value)
    {
        if (_value >= -20f)
            slider.value = _value;
    }
}
