using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    public static Data data;

    public string nameRead;
    public string bestGamer;
    public int bestScore=0;

    private void Awake()
    {
        if (data != null)
        {
            Destroy(gameObject);
            return;
        }
        data = this;
        DontDestroyOnLoad(gameObject);
        LoadBests();
    }

    [System.Serializable] public class SaveData
    {
        public int bestScore = 0;
        public string bestGamer;
    }

    public void SaveBests()
    {
        SaveData dt = new SaveData();
        dt.bestGamer = bestGamer;
        dt.bestScore = bestScore;
        string json = JsonUtility.ToJson(dt);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBests()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData dt = JsonUtility.FromJson<SaveData>(json);
            bestGamer = dt.bestGamer;
            bestScore = dt.bestScore;
        }

    }




}
