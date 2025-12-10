using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject difficultySelectionScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject aboutToExitScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapePressed();
        }
    }

    public void SelectDifficulty()
    {
        difficultySelectionScreen.SetActive(true);
        titleScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void SettingsMenu()
    {
        settingsScreen.SetActive(true);
        titleScreen.SetActive(false);
        difficultySelectionScreen.SetActive(false);
    }

    public void TitleScreen()
    {
        titleScreen.SetActive(true);
        difficultySelectionScreen.SetActive(false);
        settingsScreen.SetActive(false);
        aboutToExitScreen.SetActive(false);
    }

    public void AboutToExit()
    {
        aboutToExitScreen.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void EscapePressed()
    {
        // Pressing ESC will always go to Title Screen. If already on title screen, pressing esc will activate
        // aboutToExitScreen game object so the player may quit application if they so choose.
        if (titleScreen.gameObject.activeSelf)
        {
            AboutToExit();
        }
        else if (difficultySelectionScreen.gameObject.activeSelf)
        {
            TitleScreen();
        }
        else if (settingsScreen.gameObject.activeSelf)
        {
            TitleScreen();
        }
        else if (aboutToExitScreen.gameObject.activeSelf)
        {
            TitleScreen();
        }
    }

    public void ExitGame()
    {
        GameManager.gameManager.ExitGame();
    }
}
