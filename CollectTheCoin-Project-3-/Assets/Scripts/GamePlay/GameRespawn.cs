using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameRespawn : MonoBehaviour
{
    public float threshold;
    private PlayerMovement playerMovement;

    void Start()
    {
        // Get the PlayerMovement component
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            // Move the player to the respawn position
            transform.position = new Vector3(0f, 2.16f, 0f);

            // Handle damage (reduce life)
            if (playerMovement != null)
            {
                Debug.Log("Handling damage...");
                playerMovement.HandleDamage();
            }
        }

    }
}


