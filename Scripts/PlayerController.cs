using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameController gameController;
    private Rigidbody playerRb;
    private Vector3 direction;
    private bool isMoving;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            playerRb.AddForce(direction * speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (gameController.gameActive)
        {
            GetInput();
        }
    }

    private void GetInput()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += new Vector3(0, 0, -1);
        }

        isMoving = direction != Vector3.zero;
        direction.Normalize(); // Ensure the direction vector is normalized so the speed remains consistent.
    }
}