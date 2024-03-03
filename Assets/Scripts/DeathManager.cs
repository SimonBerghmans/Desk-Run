using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathManager : Singleton
{
    // Start is called before the first frame update
    public Text timeText;
    private float minutesReached;
    private float secondsReached;

    private void Awake()
    {
        GetValues();
        UpdateUI();
    }
    private void UpdateUI()
    {
        timeText.text = "Time reached: " + Mathf.Round(minutesReached).ToString() + ":" + Mathf.Round(secondsReached).ToString();
    }
    
    private void GetValues()
    {
        minutesReached = GameManager.timerMinutes;
        secondsReached = GameManager.timerSeconds;
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
