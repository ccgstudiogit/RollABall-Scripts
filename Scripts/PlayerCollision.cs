using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isFalling;
    
    [SerializeField] private AudioSource fallingWhistle;
    private GameController gameController;
    private CameraController cameraController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();

        if (fallingWhistle == null)
        {
            Debug.Log("Make sure fallingWhistle has an audio source in PlayerCollision.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallZone"))
        {
            isFalling = true;
            fallingWhistle.Play();
            gameController.GameOver();
            cameraController.GetPlayerPosition();
            Debug.Log("Player fell.");
        }

        if (other.CompareTag("VictoryZone"))
        {
            gameController.PlayerWin();
            Debug.Log("Player wins.");
        }
    }
}