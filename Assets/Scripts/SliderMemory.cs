using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMemory : MonoBehaviour
{

    public Slider sensibilitySlider;
    public Slider musicSlider;
    public Slider SFX_Slider;
    // Start is called before the first frame update
    public void Awake()
    {
        sensibilitySlider.value = PlayerPrefs.GetFloat("sensibility");
        musicSlider.value = PlayerPrefs.GetFloat("music");
        SFX_Slider.value = PlayerPrefs.GetFloat("SFX");

    }
}
