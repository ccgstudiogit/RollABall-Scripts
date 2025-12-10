using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryAreaPosition : MonoBehaviour
{
    [SerializeField] private GameObject startingPlatform;
    private GameController gameController;
    private Vector3 startingPlatformPos;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        startingPlatformPos = startingPlatform.transform.position;
        MovePosition();
    }

    private void MovePosition()
    {
        transform.position = new Vector3(startingPlatformPos.x + gameController.distanceBetweenPlatforms, transform.position.y, transform.position.z);
    }
}
