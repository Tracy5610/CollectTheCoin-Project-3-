using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCubeSpawner : MonoBehaviour
{
    public GameObject bulletCubePrefab; // Reference to the bullet cube prefab
    public float spawnInterval = 10f; // Time interval between spawns
    private GameObject player;

    // Define the boundaries of the spawning area
    public float minX = -4f;
    public float maxX = 4f;
    public float minZ = -4f;
    public float maxZ = 4f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnBulletCubes());
    }

    private IEnumerator SpawnBulletCubes()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnBulletCube();
        }
    }

    private void SpawnBulletCube()
    {
        if (bulletCubePrefab == null || player == null) return;

        Vector3 spawnPosition;
        int attempts = 0;
        bool positionFound = false;

        do
        {
            attempts++;
            // Generate random position within the defined boundaries
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            spawnPosition = new Vector3(randomX, player.transform.position.y, randomZ);

            // Check if the position is a safe distance from the player
            if (Vector3.Distance(player.transform.position, spawnPosition) > 5f) // Adjust distance as needed
            {
                positionFound = true;
            }

        } while (!positionFound && attempts < 10); // Limit the number of attempts to find a valid position

        if (positionFound)
        {
            Instantiate(bulletCubePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Bullet cube spawned at: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("Failed to find a suitable spawn position for the bullet cube.");
        }
    }
}
