using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        EventManager.Instance.Init();
        DataManager.Instance.Init();
    }
    private void Start()
    {
        InitializeByOrder();
    }
    void InitializeByOrder()
    {
        SaveManager.Instance.Init();
        FishSpawner.Instance.Init();
        BoidsManager.Instance.Init();
    }
}
