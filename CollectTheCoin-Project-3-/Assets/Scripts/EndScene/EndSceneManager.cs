using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public Text finalScoreText;
    public Text highScoresText;

    private void Start()
    {
        int finalScore = CoinCounter.instance.GetCurrentScore();
        finalScoreText.text = "Final Score: " + finalScore;

        // Retrieve and display high scores
        List<HighscoreEntry> highScores = HighscoreManager.instance.GetHighscores();
        highScoresText.text = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoresText.text += (i + 1) + ". " + highScores[i].playerName + ": " + highScores[i].score + "\n";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
