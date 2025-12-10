using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    
    public Difficulty selectedDifficulty { get; private set; }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Insane
    }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        selectedDifficulty = difficulty;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadMainMenu()
    {
        // Makes sure the ball animation is playing when player returns to main menu after pausing game
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        AudioManager.Instance.ResetVolumes();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}