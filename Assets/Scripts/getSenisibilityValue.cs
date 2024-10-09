using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSenisibilityValue : MonoBehaviour
{
    // Start is called before the first frame update
    public void getSliderValue(float newSensib)
    {
        PlayerPrefs.SetFloat("sensibility", newSensib);
    }
}
