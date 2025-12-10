using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    
    public enum VolumeType { Music, SoundEffects }
    public VolumeType volumeType;

    private void Start()
    {
        slider = GetComponent<Slider>();
        float volume = 1f;

        if (volumeType == VolumeType.Music)
        {
            volume  = PlayerPrefs.GetFloat("musicVol", 1f);
        }
        else if (volumeType == VolumeType.SoundEffects)
        {
            volume = PlayerPrefs.GetFloat("sfxVol", 1f);
        }

        slider.value = volume;
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            if (volumeType == VolumeType.Music)
            {
                AudioManager.Instance.SetMusicVolume(volume);
            }
            else if (volumeType == VolumeType.SoundEffects)
            {
                AudioManager.Instance.SetSFXVolume(volume);
            }
        }
    }
}
