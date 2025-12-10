using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioMixer mixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load volume settings from PlayerPrefs
        float musicVolume = PlayerPrefs.GetFloat("musicVol" , 1f);
        float sfxVolume = PlayerPrefs.GetFloat("sfxVol" , 1f);
        string volumeOn = PlayerPrefs.GetString("globalVolume", "on");

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void EnableSound(bool enabled)
    {
        if (enabled == false)
        {
            mixer.SetFloat("masterVol", -80);
            PlayerPrefs.SetString("globalVolume", "off");
        }
        else if (enabled == true)
        {
            mixer.SetFloat("masterVol", 0);
            PlayerPrefs.SetString("globalVolume", "on");
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        // Converts to logarithm to the base of 10. This is done because it takes the slider value 0.0001 to 1
        // and turns it into a value between -80 and 0 but on a logarithmic scale
        mixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVol", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVol", sliderValue);
    }

    public void ResetVolumes()
    {
        PlayerPrefs.SetFloat("musicVol", 1f);
        PlayerPrefs.SetFloat("sfxVol", 1f);
        PlayerPrefs.SetString("globalVolume", "on");
    }
}
