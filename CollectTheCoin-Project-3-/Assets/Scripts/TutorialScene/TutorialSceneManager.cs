using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    public void IntroGame()
    {
        // Load the Intro scene 
        SceneManager.LoadScene("IntroScene");
    }
}
