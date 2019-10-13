using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Text highScore;
    public Text highTime;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetString("HighScoreTime", "00:00:00");
            PlayerPrefs.SetInt("HighScoreTimeInt", 0);
        }
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        highTime.text = PlayerPrefs.GetString("HighScoreTime");
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
