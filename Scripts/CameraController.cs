using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameController gameController;
    private PlayerCollision playerCollision;
    private Vector3 offset;
    private Vector3 moveTarget;
    private Vector3 moveTargetOffset = new Vector3(2.5f, 0, 0); // Moves camera on player death
    private Quaternion rotateTarget = Quaternion.Euler(75, 90, 0); // Rotates camera on player death
    private float smooth = 5;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        if (player != null)
        {
            playerCollision = player.GetComponent<PlayerCollision>();
        }
        else
        {
            Debug.Log("Please attach player object to player in CameraController.");
        }

        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (!playerCollision.isFalling && gameController.gameActive)
        {
            transform.position = player.transform.position + offset;
        }
        else if (playerCollision.isFalling)
        {
            PlayerDeath();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        moveTarget = transform.position + new Vector3(2, 0, 0);
        return moveTarget;
    }

    private void PlayerDeath()
    {
        // On player death, the camera stops following the player, moves forward slightly, then rotates downwards as the player falls
        transform.position = transform.position;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, moveTarget.x, Time.deltaTime * (smooth / 1.5f)), transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, Time.deltaTime * smooth);
    }
}