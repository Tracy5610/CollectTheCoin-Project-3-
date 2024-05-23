using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyClone"))
        {
            // Hide or disable the enemy when hit by the bullet
            other.gameObject.SetActive(false);
            // destroy the bullet when it hits the enemy
            // Destroy(gameObject);
        }
    }
}

