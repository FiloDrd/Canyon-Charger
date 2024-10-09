using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SFXSlider : MonoBehaviour
{
    public AudioMixer sfxMixer;

    public AudioClip pop;

    private void Start()
    {
        sfxMixer.SetFloat("Sfx", PlayerPrefs.GetFloat("SFX"));
    }

    public void SetSFX (float sfx)
    {
        PlayerPrefs.SetFloat("SFX", sfx);
        sfxMixer.SetFloat("Sfx", sfx);
    }

    

    
}
