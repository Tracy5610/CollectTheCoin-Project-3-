using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the coin count in the CoinCounter instance
            CoinCounter.instance.IncrementCoinCount();
            HideCoin();
        }
    }

    private void HideCoin()
    {
        // Disable the coin GameObject instead of destroying it
        gameObject.SetActive(false);
    }

    public void ShowCoin()
    {
        // Enable the coin GameObject when it needs to be shown again
        gameObject.SetActive(true);
    }
}
