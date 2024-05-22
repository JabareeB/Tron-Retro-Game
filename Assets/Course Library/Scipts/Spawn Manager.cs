using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawnManager : MonoBehaviour
{
    public GameObject opponentLightCyclePrefab; // Assign this in the inspector
    private Vector3 spawnPosition = new Vector3(-39.27f, 0f, -28.67f);
    private Quaternion spawnRotation = Quaternion.identity; // No rotation, facing upwards

    void Start()
    {
        SpawnOpponent();
    }

    void SpawnOpponent()
    {
        // Instantiate the opponent light cycle at the specified spawn position and rotation
        Instantiate(opponentLightCyclePrefab, spawnPosition, spawnRotation);
    }
}

