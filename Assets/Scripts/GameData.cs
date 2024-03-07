using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Start is called before the first frame update

    public float achievedMinutes;
    public float achievedSeconds;

    public GameData(GameManager game)
    {
        achievedMinutes = game.timerMinutes;
        achievedSeconds = game.timerSeconds;
    }

}
