using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component and handle damage
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.HandleDamage();
            }

            // Destroy the bullet when it hits the player
            Destroy(gameObject);
        }
    }
}
