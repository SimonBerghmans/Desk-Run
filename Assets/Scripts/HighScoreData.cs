using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
    public float achievedMinutes;
    public float achievedSeconds;

    public HighScoreData(DeathManager death)
    {
        achievedMinutes = death.minutesReached;
        achievedSeconds = death.secondsReached;
    }
}
