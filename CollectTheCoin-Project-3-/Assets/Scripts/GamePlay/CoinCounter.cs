using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;

    public Text coinText;
    private int coinCount = 0;

    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;

    private void Awake()
    {
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

    private void Start()
    {
        if (coinText == null)
        {
            Debug.LogError("coinText is not assigned in the inspector.");
        }
        UpdateCoinText();
    }

    public void ResetCoins()
    {
        coinCount = 0;
        UpdateCoinText();
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateCoinText();

        if (coinCount % 10 == 0)
        {
            SpawnEnemy();
        }
    }

    public int GetCurrentScore() // Ensure this method is present
    {
        return coinCount;
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogError("coinText is null. Cannot update coin text.");
        }
    }

    private void SpawnEnemy()
    {
        if (enemySpawnPoints.Length > 0)
        {
            Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            GameObject enemyClone = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemyClone.tag = "EnemyClone";
            if (enemyClone.TryGetComponent<Renderer>(out var renderer))
            {
                renderer.material.color = Color.blue;
            }
            Debug.Log("New enemy spawned at position: " + spawnPoint.position);
        }
        else
        {
            Debug.LogError("No enemy spawn points available.");
        }
    }

   

}
