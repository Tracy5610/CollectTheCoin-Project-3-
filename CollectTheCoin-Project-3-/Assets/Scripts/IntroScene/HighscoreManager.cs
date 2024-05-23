using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;

    private readonly List<HighscoreEntry> highscoreList = new List<HighscoreEntry>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadHighscores();
    }

    public void AddNewScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        HighscoreEntry newEntry = new HighscoreEntry { playerName = playerName, score = score };
        highscoreList.Add(newEntry);
        highscoreList.Sort((x, y) => y.score.CompareTo(x.score)); // Sort by score descending

        if (highscoreList.Count > 10)
        {
            highscoreList.RemoveAt(10); // Keep only top 10 scores
        }

        SaveHighscores();
    }

    public List<HighscoreEntry> GetHighscores()
    {
        return highscoreList;
    }

    private void SaveHighscores()
    {
        for (int i = 0; i < highscoreList.Count; i++)
        {
            PlayerPrefs.SetString("HighscoreName" + i, highscoreList[i].playerName);
            PlayerPrefs.SetInt("HighscoreScore" + i, highscoreList[i].score);
        }
    }

    private void LoadHighscores()
    {
        highscoreList.Clear();
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey("HighscoreName" + i) && PlayerPrefs.HasKey("HighscoreScore" + i))
            {
                HighscoreEntry entry = new HighscoreEntry
                {
                    playerName = PlayerPrefs.GetString("HighscoreName" + i),
                    score = PlayerPrefs.GetInt("HighscoreScore" + i)
                };
                highscoreList.Add(entry);
            }
        }
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string playerName;
    public int score;
}
