using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = true;

        string volume = PlayerPrefs.GetString("globalVolume");

        if (volume == "on")
        {
            toggle.isOn = true;
        }
        else if (volume == "off")
        {
            toggle.isOn = false;
        }

        toggle.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(bool isOn)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.EnableSound(isOn);

            // condition ? value_if_true : value_if_false (basically a quicker way to write a small if-else statement)
            string volume = isOn ? "on" : "off";
            PlayerPrefs.SetString("globalVolume", volume);
        }
    }
}
