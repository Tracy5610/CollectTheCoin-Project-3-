using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeDisplay : MonoBehaviour
{
    // Variables 
    public PlayerMovement player;
    public Text lifeText;

    private void Start()
    {
        if (player != null)
        {
            OnLifeCountChanged();
        }
    }
    // Display the change in life
    public void OnLifeCountChanged()
    {
        if (lifeText != null)
        {
            lifeText.text = "Lives: " + player.CurrentLives;
        }
    }
}
