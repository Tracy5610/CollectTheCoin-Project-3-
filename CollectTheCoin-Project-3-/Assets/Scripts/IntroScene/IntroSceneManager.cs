using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public InputField nameInputField;

    private void Start()
    {
        if (HighscoreManager.instance == null)
        {
            new GameObject("HighscoreManager").AddComponent<HighscoreManager>();
        }

        // Ensure the input field is empty initially
        nameInputField.text = "";
    }

    // Load Game Scene
    public void StartGame()
    {
        string playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        // Store player name for use in the game
        PlayerPrefs.SetString("PlayerName", playerName);

        SceneManager.LoadScene("GameScene");
    }

    // Load Tutorial Scene
    public void ShowTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}


