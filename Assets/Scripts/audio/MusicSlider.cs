using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicSlider : MonoBehaviour
{
    public AudioMixer musicMixer;

    private void Start()
    {
        musicMixer.SetFloat("Music", PlayerPrefs.GetFloat("music"));
    }

    public void SetMusic (float music)
    {
        PlayerPrefs.SetFloat("music", music);
        musicMixer.SetFloat("Music", PlayerPrefs.GetFloat("music"));
    }
}
