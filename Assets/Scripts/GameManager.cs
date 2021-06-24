using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string DATA_FILE_NAME = "/savefile.json";
    public static GameManager Instance;
    // Start is called before the first frame update

    public string playerName;
    public HighScoreInfo highScore = new HighScoreInfo();

    private void Awake()
    {      
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadPersistentInfo();
        
    }

    private void LoadPersistentInfo()
    {
        string path = Application.persistentDataPath + DATA_FILE_NAME;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;            
        }
    }


    public void SavePersistentInfo()
    {
        SaveData data = new SaveData();

        data.highScore = this.highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + DATA_FILE_NAME, json);
    }

}
[Serializable]
public class HighScoreInfo
{
    public string playerName;
    public int points;
}
[Serializable]
public class SaveData
{
    public HighScoreInfo highScore = new HighScoreInfo();
    
}