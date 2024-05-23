using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to respawn coins
    public void StartRespawnCoroutine(GameObject coinPrefab, float respawnTime, Vector3 originalPosition)
    {
        StartCoroutine(RespawnCoin(coinPrefab, respawnTime, originalPosition));
    }

    private IEnumerator RespawnCoin(GameObject coinPrefab, float respawnTime, Vector3 originalPosition)
    {
        // Wait for the specified respawn time
        yield return new WaitForSeconds(respawnTime);

        // Define the upright rotation (90 degrees in X to stand the coin upright)
        Quaternion uprightRotation = Quaternion.Euler(90, 0, 0);

        // Define the bounds for spawning coins
        float minX = -4.0f;
        float maxX = 4.0f;
        float minZ = -4.0f;
        float maxZ = 4.0f;

        // Spawn a new coin at a random position within the defined bounds
        Vector3 randomPosition = new(
            Random.Range(minX, maxX),
            originalPosition.y,
            Random.Range(minZ, maxZ)
        );

        GameObject newCoin = Instantiate(coinPrefab, randomPosition, uprightRotation);

        // Ensure the new coin is active
        newCoin.SetActive(true);
    }
}
