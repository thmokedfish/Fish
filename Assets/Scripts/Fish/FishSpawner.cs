using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FishSpawner : MonoBehaviour
{
    public string LocalPath = "FishData/FishData.json";
    public int depthCount = 3;  //比如maxDepth为3,则包括0,1,2三个深度
    public float range;
    public float fishInterval;
    private Pool<FishMove> pool;
    private FishDataList LoadedData;
   // private GameObject[] FishPrefabs;
    private List<List<FishData>> depthList = new List<List<FishData>>();
    private List<List<JellyData>> jellyDepthList = new List<List<JellyData>>();
    public FishMove targetPrefab; //用于传递给pool
    private void Start()
    {
        pool = new Pool<FishMove>(targetPrefab);
        LoadJson();
        InitDepthLists();
        ShowLoadedData();
         RandomSpawnFish(2, Vector3.zero);
         RandomSpawnFish(1, Vector3.zero);

    }
    private void LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, LocalPath);
        if (!File.Exists(filePath))
        {
            Debug.LogError("incorrect file path!");
            return;
        }
        JsonReader.LoadJson<FishDataList>(filePath,out LoadedData);
    }
    private void InitDepthLists()
    {
        //二维lists初始化
        for (int i = 0; i < depthCount; i++)
        {
            depthList.Add(new List<FishData>());
            jellyDepthList.Add(new List<JellyData>());
        }
        //为每个list添加成员
        foreach (FishData data in LoadedData.datalist)
        {
            if (data.depth >= depthCount || data.depth < 0)
            {
                Debug.LogError("depth error");
                data.depth = depthCount - 1;
            }
            depthList[data.depth].Add(data);
        }
        foreach(JellyData data in LoadedData.jellyDatas)
        {
            if (data.depth >= depthCount || data.depth < 0)
            {
                Debug.LogError("depth error");
                data.depth = depthCount - 1;
            }
            jellyDepthList[data.depth].Add(data);
        }
    }
    public void RandomSpawnFish(int depth, Vector3 centerPos)
    {
        if(depth>=depthCount||depth<0)
        {
            Debug.LogError("depth out of range");
            return;
        }
        int random = Random.Range(0, depthList[depth].Count);
        FishData data = depthList[depth][random];
        SpawnFish(data,Vector3.zero);
    }

    public void SpawnFish(int depth,int index,Vector3 center)
    {
        if (depth >= depthCount || depth < 0)
        {
            Debug.LogError("depth out of range");
            return;
        }
        if (index>=depthList[depth].Count||index<0)
        {
            Debug.LogError("index out of range");
            return;
        }
        FishData data = depthList[depth][index];
        SpawnFish(data,center);
    }

    public void SpawnFish(FishData data,Vector3 center)
    {
        float x = Random.Range(0, range);
        float z = range - x;
        int r = Random.Range(0, 2);
        Debug.Log(r);
        if (r == 0) { x *= -1; }
        r = Random.Range(0, 2);
        Debug.Log(r);
        if (r == 0) { z *= -1; }
        Vector3 pos = new Vector3(center.x + x, center.y + Random.Range(-range, range), center.z + z);

        //spawn target
        FishMove target = pool.Spawn();
        target.transform.rotation = Quaternion.LookRotation(new Vector3(-x, 0, -z), Vector3.up);
        target.data = data;

        string fishname = data.name;
        int fishNum = Random.Range(data.minPopulation, data.maxPopulation + 1);
        for (int i = 0; i < fishNum; i++)
        {
            GameObject fish = Instantiate(Resources.Load<GameObject>("Fishes/" + fishname),pos,Quaternion.identity);  //后续加入范围内根据target角度随机position,以及多条生成防止重合
                                                                                              //FishMove move=fish.AddComponent<FishMove>();
                                                                                              // move.data = data;
            FollowFishMove follow = fish.GetComponent<FollowFishMove>();
            if (!follow)
            {
                follow=fish.AddComponent<FollowFishMove>();
                Debug.Log(fish.name + " no follow component!");
            }
            follow.target = target;
            pos += new Vector3(Random.value, Random.value, Random.value)*fishInterval;
        }
    }
    public void ShowLoadedData()
    {
        if (LoadedData == null) { return; }
        Debug.Log("loaded data length:" + LoadedData.datalist.Length);
        Debug.Log("jelly num:" + LoadedData.jellyDatas.Length);
        foreach (FishData data in LoadedData.datalist)
        {
            Debug.Log(data.name + " " + data.info);
        }
    }
}

/*
public class FishSpawner : MonoBehaviour
{
    public FishMove prefab;
    Pool<FishMove> fishPool;
    public FishData[] fishData;
    private void Start()
    {
        fishPool = new Pool<FishMove>(prefab);
        LoadData();
        Debug.Log("fish data length:"+fishData.Length);
        for(int i=0;i<fishData.Length;i++)
        {
            SpawnFish(new Vector3(0,3*i,0), new Quaternion(), fishData[i]);
        }
    }

    private void LoadData()
    {
        fishData = Resources.LoadAll<FishData>("ScriptableObjects");
    }

    public void SpawnFish(Vector3 pos,Quaternion rot,FishData fishData)
    {
        FishMove fish=fishPool.Spawn();
        fish.data = fishData;
        fish.transform.position = pos;
        fish.transform.rotation = rot;
    }
}
*/