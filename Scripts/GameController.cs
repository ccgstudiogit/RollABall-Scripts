using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameActive;
    public float distanceBetweenPlatforms = 0;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject directionalLight;
    [SerializeField] private GameObject easyMediumHardLight;
    [SerializeField] private GameObject insaneLight;
    [SerializeField] private float easyDistance = 25;
    [SerializeField] private float mediumDistance = 35;
    [SerializeField] private float hardDistance = 45;
    [SerializeField] private float insaneDistance = 50;

    void Start()
    {
        Debug.Log($"Current difficulty: {GameManager.gameManager.selectedDifficulty}");

        switch (GameManager.gameManager.selectedDifficulty)
        {
            case GameManager.Difficulty.Easy:
                distanceBetweenPlatforms = easyDistance;
                StartGame();
                break;
            case GameManager.Difficulty.Medium:
                distanceBetweenPlatforms = mediumDistance;
                StartGame();
                break;
            case GameManager.Difficulty.Hard:
                distanceBetweenPlatforms = hardDistance;
                StartGame();
                break;
            case GameManager.Difficulty.Insane:
                distanceBetweenPlatforms = insaneDistance;
                directionalLight.SetActive(false);
                easyMediumHardLight.SetActive(false);
                insaneLight.SetActive(true);
                StartGame();
                break;
        }
    }
    
    public void StartGame()
    {
        gameActive = true;
        pauseButton.gameObject.SetActive(true);

        Debug.Log("Game started!");
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        gameActive = false;

        Debug.Log("Game over!");
    }

    public void PlayerWin()
    {
        victoryScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        gameActive = false;

        Debug.Log("Player wins!");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Game reset.");
    }

    public void ReturnToMainMenu()
    {
        GameManager.gameManager.LoadMainMenu();
    }
}
