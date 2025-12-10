using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private GameObject victoryPlatform;
    private BoxCollider victoryZoneCollider;
    private float heightBuffer = 0.5f;

    void Start()
    {
        victoryZoneCollider = GetComponent<BoxCollider>();

        if (victoryPlatform != null)
        {
            CalculateVictoryZoneSize();
            CalculateVictoryZonePosition();
        }
        else
        {
            Debug.LogWarning("Error: Make sure victoryPlatform is assigned in VictoryZone.");
        }
    }

    private void CalculateVictoryZoneSize()
    {
        float length;
        float width;
        float height;

        length = victoryPlatform.transform.localScale.x;
        height = victoryPlatform.transform.localScale.y + heightBuffer;
        width = victoryPlatform.transform.localScale.z;

        victoryZoneCollider.size = new Vector3(length - 2, height, width);
    }

    private void CalculateVictoryZonePosition()
    {
        transform.position = victoryPlatform.transform.position + new Vector3(0, heightBuffer, 0);
    }
}