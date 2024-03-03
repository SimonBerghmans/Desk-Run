using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton
{
    private GameObject player;
    private Player playerScript;
    public GameObject zombie;
    public Transform spawnPoint;
    public List <Transform> enemySpawnPoints;
    private Text timerText;
    private Text timeToNextText;
    private Text roundText;
    private Text zombiesText;
    static public float timerSeconds;
    static public float timerMinutes;
    static public float activeZombies;
    private float maxZombies;
    private float timeTillNextRound;
    private float timeTillNextTimer;
    private int round;
    private bool roundTimerRunning;

    // Start is called before the first frame update
    void Start()
    {
        SetGameManagerValues();
        SetPlayerLocation();
        UpdateRoundInfoText();
    }
 
    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        CheckDead();
        CheckAndSpawnZombies();
        if (!roundTimerRunning)
        {
            StartCoroutine(IncreaseRound());
        }
        UpdateTimeTillNextText();
    }
    private void SetPlayerLocation()
    {
        player.transform.position = spawnPoint.position;
    }
    private void CheckDead()
    {
        if(playerScript.isDead)
        {
            GoToDeathScreen();
        }
    }
    private void SetGameManagerValues()
    {
        player = GameObject.FindWithTag("Player");
        timerText = GameObject.Find("Timer Text").GetComponent<Text>();
        zombiesText = GameObject.Find("Zombies Text").GetComponent<Text>();
        timeToNextText = GameObject.Find("Time Till Next Text").GetComponent<Text>();
        roundText = GameObject.Find("Round Text").GetComponent<Text>();
        playerScript = player.GetComponentInChildren<Player>();
        timerSeconds = 0;
        timerMinutes = 0;
        maxZombies = 30;
        activeZombies = 0;
        round = 1;
        timeTillNextRound = 30;
        roundTimerRunning = false;
        timeTillNextTimer = timeTillNextRound;
        
    }
    private void UpdateTimer()
    {
        timerSeconds += Time.deltaTime;
        if (timerSeconds >= 60)
        {
            timerSeconds = 0;
            timerMinutes++;
        }
        if (timerSeconds <= 9)
        {
            timerText.text = Mathf.Round(timerMinutes).ToString() + ":0" + Mathf.Round(timerSeconds).ToString();
        }
        else
        {
            timerText.text = Mathf.Round(timerMinutes).ToString() + ":" + Mathf.Round(timerSeconds).ToString();
        }
    }
    private void GoToDeathScreen()
    {
        SceneManager.LoadScene(2);
    }
    private void CheckAndSpawnZombies()
    {
        if (activeZombies <= maxZombies)
        {
            int randomSpawnNum = Random.Range(0,enemySpawnPoints.Count);
            Transform randomSpawnPoint = enemySpawnPoints[randomSpawnNum];
            Instantiate(zombie, randomSpawnPoint);
            activeZombies++;
        }
    }
    IEnumerator IncreaseRound()
    {
        {
            roundTimerRunning = true;
            yield return new WaitForSeconds(timeTillNextRound);
            maxZombies *= Mathf.Pow(1.20f, round);
            maxZombies = Mathf.Round(maxZombies);
            round++;
            timeTillNextRound += round * 10;
            timeTillNextTimer = timeTillNextRound;
            UpdateRoundInfoText();
            roundTimerRunning = false;
        }
        
     }
    private void UpdateRoundInfoText()
    {
        roundText.text = round.ToString();
        zombiesText.text = maxZombies.ToString();
        
    }
    private void UpdateTimeTillNextText()
    {
        timeTillNextTimer -= Time.deltaTime;
        timeToNextText.text = (Mathf.Round(timeTillNextTimer).ToString());
    }
        

}
