using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    [SerializeField] private GameObject startingPlatform;
    [SerializeField] private GameObject victoryPlatform;
    private BoxCollider fallZoneCollider;
    private float yOffset = 1.5f;
    private float widthBuffer = 10;

    void Start()
    {
        fallZoneCollider = GetComponent<BoxCollider>();

        if (startingPlatform != null && victoryPlatform != null)
        {
            CalculateFallZonePosition();
            CalculateFallZoneSize();
        }
        else
        {
            Debug.Log("Error: Please assign starting platform and victory platform in FallZone script.");
        }
    }

    private void CalculateFallZoneSize()
    {
        float length;
        float width;

        length = Vector3.Distance(startingPlatform.transform.position, victoryPlatform.transform.position);
        width = startingPlatform.transform.localScale.z;

        fallZoneCollider.size = new Vector3(length, 1, width + widthBuffer);
    }

    private void CalculateFallZonePosition()
    {
        transform.position = new Vector3(Mathf.Lerp(startingPlatform.transform.position.x, victoryPlatform.transform.position.x, 0.5f), startingPlatform.transform.position.y - yOffset, startingPlatform.transform.position.z);
    }
}