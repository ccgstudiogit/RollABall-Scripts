using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    [SerializeField] private GameObject pathNodePrefab;
    [SerializeField] private GameObject startingPlatform;
    [SerializeField] private GameObject victoryPlatform;
    [SerializeField] private int straighterPathMultiplier;

    private void Start()
    {
        if (ReadyToGenerate() && IsOrderedCorrectly())
        {
            GeneratePath();
        }
    }

    private void GeneratePath()
    {
        Vector3[] startArea;
        Vector3[] endArea;
        Vector3 currentPos;
        Vector3 targetPos;
        Vector3 previousPos = Vector3.zero;
        float maxZ;
        float minZ;
        int wentStraight = 0;
        bool isGenerating = true;

        startArea = GetStartingPlatformSpawnArea();
        endArea = GetVictoryPlatformEndArea();
        maxZ = MaximumZ();
        minZ = MinimumZ();

        // Randomly selects valid spawn point from start area and spawns 2 initial path nodes
        currentPos = startArea[Random.Range(0, startArea.Length - 1)];
        Instantiate(pathNodePrefab, currentPos, Quaternion.identity);
        currentPos += new Vector3(1, 0, 0);
        Instantiate(pathNodePrefab, currentPos, Quaternion.identity);

        while (isGenerating)
        {
            List<Vector3> validPositions = new List<Vector3>();
            Vector3 validPos;

            // Checks if going right is valid
            if (currentPos.z > minZ + 1 && previousPos != currentPos + new Vector3(0, 0, -1))
            {
                validPos = currentPos + new Vector3(0, 0, -1);
                validPositions.Add(validPos);
            }
            // Checks if going left is valid
            if (currentPos.z < maxZ - 1 && previousPos != currentPos + new Vector3(0, 0, 1))
            {
                validPos = currentPos + new Vector3(0, 0, 1);
                validPositions.Add(validPos);
            }
            // Checks if going straight is valid 
            if (wentStraight <= straighterPathMultiplier)
            {
                validPos = currentPos + new Vector3(1, 0, 0);
                validPositions.Add(validPos);
            }

            previousPos = currentPos;
            targetPos = validPositions[Random.Range(0, validPositions.Count)];
            currentPos = targetPos;

            // Checks if path went straight
            if (previousPos == currentPos + new Vector3(-1, 0, 0))
            {
                wentStraight++;
            }
            // Checks if path went right
            else if (previousPos == currentPos + new Vector3(0, 0, 1))
            {
                wentStraight = 0;
            }
            // Checks if path went left
            else if (previousPos == currentPos + new Vector3(0, 0, -1))
            {
                wentStraight = 0;
            }

            Instantiate(pathNodePrefab, currentPos, Quaternion.identity);

            //Checks if path has reach the end area
            for (int i = 0; i < endArea.Length; i++)
            {
                if (currentPos == endArea[i])
                {
                    isGenerating = false;
                }
            }
        }
    }

    private Vector3[] GetStartingPlatformSpawnArea()
    {
        Vector3[] spawnAreaPoints = new Vector3[Mathf.RoundToInt(startingPlatform.transform.localScale.z)];
        Vector3 initialPosition;

        float xPoint = startingPlatform.transform.position.x;
        float yPoint = startingPlatform.transform.position.y;
        float zPoint = startingPlatform.transform.position.z;

        // 0.5 is the offset to make sure the Vector3 point is in the middle of a grid point square
        xPoint = xPoint + startingPlatform.transform.localScale.x / 2 + 0.5f;
        zPoint = zPoint - startingPlatform.transform.localScale.z / 2 + 0.5f;

        initialPosition = new Vector3(xPoint, yPoint, zPoint);
        spawnAreaPoints[0] = initialPosition;

        for (int i = 1; i < spawnAreaPoints.Length; i++)
        {
            spawnAreaPoints[i] = initialPosition + new Vector3(0, 0, i);
        }

        return spawnAreaPoints;
    }

    private Vector3[] GetVictoryPlatformEndArea()
    {
        Vector3[] endAreaPoints = new Vector3[Mathf.RoundToInt(victoryPlatform.transform.localScale.z)];
        Vector3 initialPosition;

        float xPoint = victoryPlatform.transform.position.x;
        float yPoint = victoryPlatform.transform.position.y;
        float zPoint = victoryPlatform.transform.position.z;
        
        xPoint = xPoint - victoryPlatform.transform.localScale.x / 2 - 0.5f;
        zPoint = zPoint - victoryPlatform.transform.localScale.z / 2 + 0.5f;

        initialPosition = new Vector3(xPoint, yPoint, zPoint);
        endAreaPoints[0] = initialPosition;

        for (int i = 1; i < endAreaPoints.Length; i++)
        {
            endAreaPoints[i] = initialPosition + new Vector3(0, 0, i);
        }

        return endAreaPoints;
    }

    private bool ReadyToGenerate()
    {
        if (startingPlatform.transform.position.y == victoryPlatform.transform.position.y)
        {
            if (startingPlatform.transform.position.z == victoryPlatform.transform.position.z)
            {
                if (startingPlatform.transform.localScale == victoryPlatform.transform.localScale)
                {
                    if (pathNodePrefab != null && startingPlatform != null && victoryPlatform != null)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.Log("Error: Cannot generate path. Make sure pathNodePrefab, startingPlatform, and victoryPlatform are not null in PathGeneration.");
                        return false;
                    }
                }
                else
                {
                    Debug.Log("Error: Cannot generate path. Make sure starting platform and victory platform have the same scaling.");
                    return false;
                }
            }
            else
            {
                Debug.Log("Error: Cannot generate path. Make sure starting platform and victory platform positions have the same z value.");
                return false;
            }
        }
        else
        {
            Debug.Log("Error: Cannot generate path. Make sure starting platform and victory platform are on the same y level.");
            return false;
        }
    }

    private bool IsOrderedCorrectly()
    {
        if (startingPlatform.transform.position.x < victoryPlatform.transform.position.x)
        {
            return true;
        }
        else
        {
            Debug.Log("Error: Cannot generate path. Starting platform position must have a lower x value than the victory platform.");
            return false;
        }
    }

    private float MaximumZ()
    {
        float zValue;

        zValue = startingPlatform.transform.position.z + startingPlatform.transform.localScale.z / 2;
        return zValue;
    }

    private float MinimumZ()
    {
        float zValue;

        zValue = startingPlatform.transform.position.z - startingPlatform.transform.localScale.z / 2;
        return zValue;
    }
}