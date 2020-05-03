using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReader
{
    public static void LoadJson<T>(string filePath,out T container)
    {
        if (!File.Exists(filePath))
        {
            container = default(T);
            return;
        }
        string jsonData = File.ReadAllText(filePath,System.Text.Encoding.GetEncoding("gb2312"));//默认中文乱码
        container = JsonUtility.FromJson<T>(jsonData);
    }
}
