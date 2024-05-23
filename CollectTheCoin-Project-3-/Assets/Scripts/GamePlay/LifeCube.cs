using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.GainLife();
                Destroy(gameObject);
            }
        }
    }
}
