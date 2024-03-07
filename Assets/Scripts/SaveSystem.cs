using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    // Start is called before the first frame update
    public static void SaveData(GameManager game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Game.data";
        File.Delete(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(game);

        formatter.Serialize(stream, gameData);
        stream.Close();
        Debug.Log("saved data");
    }
    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/Game.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            Debug.Log("returned data");
            return data;

        }
        else
        {
            Debug.LogError("Savefile not found in path: " + path);
            return null;
        }
    }


    public static void saveHighsTime(DeathManager death)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Highs.data";
        File.Delete(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        HighScoreData highData = new HighScoreData(death);

        formatter.Serialize(stream, highData);
        stream.Close();
        Debug.Log("saved highs time");
    }
    public static HighScoreData LoadHighsTime()
    {
        string path = Application.persistentDataPath + "/Highs.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            HighScoreData highData = formatter.Deserialize(stream) as HighScoreData;
            stream.Close();
            Debug.Log("returned highs time");
            return highData;

        }
        else
        {
            Debug.LogError("Savefile not found in path: " + path);
            return null;
        }
    }
    
    public static void deleteStats()
    {
        string pathGame = Application.persistentDataPath + "/Game.data";
        string pathTime = Application.persistentDataPath + "/Highs.data";

        File.Delete(pathGame);
        File.Delete(pathTime);
    }
}   
