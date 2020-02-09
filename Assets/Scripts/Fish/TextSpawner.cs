using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class TextSpawner :MonoBehaviour
{
    public string LocalPath = "FishData/FishData.json";
    public FishDataList LoadedData;
    private void Start()
    {
        LoadJson();
        ShowLoadedData();
    }
    public void LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, LocalPath);
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("incorrect file path!");
            return;
        }
        string jsonData = File.ReadAllText(filePath);
        LoadedData = JsonUtility.FromJson<FishDataList>(jsonData);
        Debug.Log("loaded data length:" + LoadedData.datalist.Length);
    }
    public void ShowLoadedData()
    {
        if (LoadedData==null) { return; }
        foreach(FishData data in LoadedData.datalist)
        {
            Debug.Log(data.name + " " + data.info);
        }
    }

}