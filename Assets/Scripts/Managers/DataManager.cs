﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public HashSet<string> capturedNameSet;//用于ui，存档，扫描判定等模块
    public Dictionary<string, FishDataBase> FishDataDictionary;
    public FishDataList LoadedData;
    public string LocalPath = "FishData/FishData.json";
    [SerializeField]private int LatestCount = 3;
    public Queue<FishData> LatestCaptured;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadJson();
        InitDataDic();
        LatestCaptured = new Queue<FishData>(LatestCount);
    }

    private void Start()
    {
        EventManager.Instance.AddReferenceEvents("OnScanFinish", AddCapturedFish);
    }

    private void AddCapturedFish(object fishData)
    {
        FishData data = fishData as FishData;
        capturedNameSet.Add(data.name);
        LatestCaptured.Enqueue(data);
        if(LatestCaptured.Count>LatestCount)
        {
            LatestCaptured.Dequeue();
        }
    }

    private void LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, LocalPath);
        if (!File.Exists(filePath))
        {
            Debug.LogError("incorrect file path!");
            return;
        }
        JsonReader.LoadJson<FishDataList>(filePath, out LoadedData);
    }

    public void InitDataDic()
    {

        FishDataDictionary = new Dictionary<string, FishDataBase>(LoadedData.datalist.Length+LoadedData.jellyDatas.Length);
        foreach(FishData d in LoadedData.datalist)
        {
            FishDataDictionary.Add(d.name, d);
        }
        foreach (JellyData d in LoadedData.jellyDatas)
        {
            FishDataDictionary.Add(d.name, d);
        }
    }
}
