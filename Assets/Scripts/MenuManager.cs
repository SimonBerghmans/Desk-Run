using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton
{
    private float highMinutes;
    private float highSeconds;
    public Text highTimeText;
    private void Start()
    {
        LoadHighs();
        UpdateHighsText();
    }
    private void LoadHighs()
    {
            try
            {
                HighScoreData highTimeData = SaveSystem.LoadHighsTime();

                highMinutes = highTimeData.achievedMinutes;
                highSeconds = highTimeData.achievedSeconds;
            }
            catch
            {
                highMinutes = 0;
                highSeconds = 0;
            }
    }

    private void UpdateHighsText()
    {
        highTimeText.text = "High Score: " +  highMinutes.ToString() + ":" + highSeconds.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DeleteStats()
    {
        SaveSystem.deleteStats();
        LoadHighs();
        UpdateHighsText();
    }
}
