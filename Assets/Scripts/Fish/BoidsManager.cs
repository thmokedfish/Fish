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
    
    const int threadGroupSize = 1024;

    public ComputeShader compute;
    public BoidSettings settings;
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
        if (fishes == null) 
        {
            Debug.LogError("fishes is null!");
            return;  
        }
        int num= fishes.Length;
        BoidData[] boidData = new BoidData[num];
        for (int i = 0; i < num; i++)
        {
            boidData[i].position = fishes[i].position;
            boidData[i].direction = fishes[i].forward;
        }
        ComputeBuffer boidBuffer = new ComputeBuffer(num, BoidData.Size);
        boidBuffer.SetData(boidData);
        compute.SetBuffer(0, "boids", boidBuffer);
        compute.SetInt("numBoids", fishes.Length);
        compute.SetFloat("viewRadius", settings.perceptionRadius);
        compute.SetFloat("avoidRadius", settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(num/ (float)threadGroupSize);
        compute.Dispatch(0, threadGroups, 1, 1);

        boidBuffer.GetData(boidData);

        for (int i=0;i<num;i++)
        {
            fishes[i].avgFlockHeading = boidData[i].flockHeading;
            fishes[i].centreOfFlockmates = boidData[i].flockCentre;
            fishes[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
            fishes[i].numPerceivedFlockmates = boidData[i].numFlockmates;

            fishes[i].OnUpdate();


            //同步transform
           // fishes[i].transform.position = fishes[i].position;
          //  fishes[i].transform.rotation = Quaternion.LookRotation(fishes[i].forward, Vector3.up);
        }
        boidBuffer.Release();
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
