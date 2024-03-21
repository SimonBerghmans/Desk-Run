using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathManager : Singleton
{
    // Start is called before the first frame update
    public Text timeText;
    public Text newHighText;
    public float minutesReached;
    public float secondsReached;
    private float highMinutes;
    private float highSeconds;

    private void Awake()
    {
        LoadAchieved();
        LoadHighsTime();
        checkAndSendHigh();
        UpdateUI();
    }
    private void UpdateUI()
    {
        timeText.text = "Time reached: " + Mathf.Round(minutesReached).ToString() + ":" + Mathf.Round(secondsReached).ToString();
    }
    
    private void LoadHighsTime()
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
    private void LoadAchieved()
    {
        try
        {
            GameData data = SaveSystem.LoadData();

            minutesReached = data.achievedMinutes;
            secondsReached = data.achievedSeconds;
        }
        catch
        {
            minutesReached = 0;
            secondsReached = 0;
        }
    }

    private void checkAndSendHigh()
    {

        if (minutesReached == highMinutes && secondsReached >= highSeconds)
        {
            highMinutes = minutesReached;
            highSeconds = secondsReached;
            newHighText.enabled = true;
            SaveSystem.saveHighsTime(this);
        }
        else if (minutesReached > highMinutes)
        {
            highMinutes = minutesReached;
            highSeconds = secondsReached;
            newHighText.enabled = true;
            SaveSystem.saveHighsTime(this);
        }
        else
        {
            newHighText.enabled = false;
        }
    }


     public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
