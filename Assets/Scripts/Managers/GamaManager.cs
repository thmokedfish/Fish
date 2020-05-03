using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    void Start()
    {
        InitializeByOrder();
    }
    void InitializeByOrder()
    {
        EventManager.Instance.Init();
        DataManager.Instance.Init();
        SaveManager.Instance.Init();
        FishSpawner.Instance.Init();
        BoidsManager.Instance.Init();
    }
}
