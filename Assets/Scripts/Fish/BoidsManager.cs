using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    private static BoidsManager instance;
    public static BoidsManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    private List<FishMove> startFishes;
    private FishMove[] fishes;
    private void Awake()
    {
        instance = this;
    }
    public void Init()
    {
        startFishes = FishSpawner.Instance.AllFishes;
        fishes = startFishes.ToArray();
    }

    void Update()
    {
        UpdateBoids();
    }

    void UpdateBoids()
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            //fishes[i]
        }
    }
}
