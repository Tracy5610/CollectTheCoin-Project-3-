using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnCoin : MonoBehaviour
{
    // Variables
    public GameObject theCoinPrefab; // Reference to the coin prefab
    public float respawnTime = 5.0f; // Time before respawn
    private Vector3 originalPosition; // Original position of the coin

    private void Start()
    {
        // Store the original position of the coin
        originalPosition = transform.position;
    }

    void Update()
    {
        // Rotate the coin around the Y-axis for visual effect
        transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Increment the coin count
            CoinCounter.instance.IncrementCoinCount();

            // Deactivate the current coin
            gameObject.SetActive(false);

            // Start the respawn coroutine in the GameManager
            GameManager.instance.StartRespawnCoroutine(theCoinPrefab, respawnTime, originalPosition);
        }
    }
}


