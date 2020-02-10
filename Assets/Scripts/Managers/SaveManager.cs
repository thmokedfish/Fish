using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveManager:MonoBehaviour
{
    private FishDataList capturedFishes;
    [SerializeField] private string savePath = "save1";
    /*
    public static SaveManager Instance 
    {
        get 
        {
            if (Instance == null)
            { Instance = new SaveManager(); }
            return Instance;
        }
        private set { Instance = value; }
    }
    */
    private void Start()
    {
        Load(savePath);
        Save();
    }
    public bool Load(string path)
    {
        if(!File.Exists(path))
        {
            Debug.LogError("cant load");
            return false;
        }
        string data = File.ReadAllText(path);
        capturedFishes = JsonUtility.FromJson<FishDataList>(data);
        return true;
    }
    public void Save()
    {
        string save = JsonUtility.ToJson(capturedFishes);
        Debug.Log(save);
        StreamWriter writer = new StreamWriter(savePath);
        writer.Write(save);
        writer.Close();
    }
}
