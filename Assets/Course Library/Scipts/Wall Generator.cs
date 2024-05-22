using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // Assign your Cube Prefab here in the Inspector
    private Vector3 lastWallEndPosition;

    void Start()
    {
        lastWallEndPosition = transform.position;
        StartCoroutine(GenerateWall());
    }

    IEnumerator GenerateWall()
    {
        while(true)
        {
            CreateWallSegment();
            yield return new WaitForSeconds(0.1f); // Adjust time for wall segment generation frequency
        }
    }

    void CreateWallSegment()
    {
        GameObject wallSegment = Instantiate(wallPrefab, lastWallEndPosition, Quaternion.identity);
        lastWallEndPosition = transform.position; // Update last position to current for the next segment
    }
}
