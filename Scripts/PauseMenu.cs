using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        // If Time.timeScale is currently set to 0, set it to 1. Otherwise, set it to 0
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (Time.timeScale == 0)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }
}
