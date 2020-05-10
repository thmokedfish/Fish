using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager:MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
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
    
    public void Init()
    {
        // Load(savePath);
        // Save();

        BinaryLoad();
        //BinarySave();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        BinarySave();
    }

    public void BinaryLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(!File.Exists(savePath))
        {
            BinarySave();
        }
        FileStream fileStream = File.Open(savePath, FileMode.Open);
        
        string[] loadedNames = bf.Deserialize(fileStream) as string[];
        HashSet<string> nameHashSet = new HashSet<string>();
        for (int i = 0; i < loadedNames.Length; i++)
        {
           // Debug.Log("load " + loadedNames[i]);
            if(loadedNames[i]==null)
            {
                continue;
            }
            if(DataManager.Instance.FishDataDictionary.ContainsKey(loadedNames[i]))
            {
                nameHashSet.Add(loadedNames[i]);
            }
        }
        fileStream.Close();
        DataManager.Instance.capturedNameSet = nameHashSet;
    }

    public void BinarySave()
    {
        HashSet<string> set = DataManager.Instance.capturedNameSet;
        if(set==null)
        {
            set = new HashSet<string>();
        }
        string[] save = new string[set.Count];
        int i = 0;
        foreach(string name in set)
        {
           // Debug.Log("save "+name);
            save[i] = name;
            i++;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(savePath);
        bf.Serialize(fileStream, save);
        fileStream.Close();
    }
    /*
    public bool Load(string path)
    {
        if(!File.Exists(path))
        {
            Debug.LogError("cant load");
            return false;
        }
        string data = File.ReadAllText(path);
        capturedFishes = JsonUtility.FromJson<CapturedList>(data);
        Debug.Log(capturedFishes.nameList.Length);
        return true;
    }
    
    public void Save()
    {
        if (capturedFishes == null) { return; }
        string save = JsonUtility.ToJson(capturedFishes);
        Debug.Log(save);
        StreamWriter writer = new StreamWriter(savePath);
        writer.Write(save);
        writer.Close(); 
        
    }
    */

}
