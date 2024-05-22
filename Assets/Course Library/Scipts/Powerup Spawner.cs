using System.Collections;
using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject speedPowerUpPrefab; // Assign your speed power-up prefab in the inspector
    public GameObject invincibilityPowerUpPrefab; // Assign your invincibility power-up prefab in the inspector
    public float spawnInterval = 5f; // Time between spawns

    // Arena bounds
    private Vector3 arenaTopLeft = new Vector3(-39.13f, 0f, 58.57f);
    private Vector3 arenaBottomRight = new Vector3(39.27f, 0f, -28.67f);

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Random position within the spawn area
            Vector3 spawnPosition = new Vector3(
                Random.Range(arenaTopLeft.x, arenaBottomRight.x),
                0.5f, // Slightly above the ground to ensure it doesn't clip through
                Random.Range(arenaBottomRight.z, arenaTopLeft.z)
            );

            // Randomly choose between speed and invincibility power-ups
            GameObject powerUpPrefab = Random.value > 0.5f ? speedPowerUpPrefab : invincibilityPowerUpPrefab;
            
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

