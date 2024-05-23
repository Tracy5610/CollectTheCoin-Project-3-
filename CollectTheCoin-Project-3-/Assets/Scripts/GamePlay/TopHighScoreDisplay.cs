using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopHighScoreDisplay : MonoBehaviour
{
    public Text topHighScoreText;

    private void Start()
    {
        if (topHighScoreText == null)
        {
            Debug.LogError("TopHighScoreText is not assigned in the inspector.");
            return;
        }
        UpdateTopHighScore();
    }

    // Using the Highscore script to pull out the highscore 
    private void UpdateTopHighScore()
    {
        List<HighscoreEntry> highScores = HighscoreManager.instance.GetHighscores();
        if (highScores.Count > 0)
        {
            int topScore = highScores[0].score;
            topHighScoreText.text = "Top High Score: " + topScore;
        }
        else
        {
            topHighScoreText.text = "Top High Score: 0";
        }
    }
}
