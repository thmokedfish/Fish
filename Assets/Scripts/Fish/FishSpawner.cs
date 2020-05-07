using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FishSpawner : MonoBehaviour
{
    private static FishSpawner instance;
    public static FishSpawner Instance 
    {
        get { return instance; }
        private set { instance = value; }
    }

    public List<FishMove> AllFishes=new List<FishMove>(32);

    public string LocalPath = "FishData/FishData.json";
    public float[] yDivide;  //每个深度的y深度区间，Length-1等于深度总数(至少为1)
                             //例如length=3,值为10 0 -10,则深度1的鱼在10到0直接随机生成，深度2在0到-10
    public float timeInterval = 2f;
    private int playerDepth = 0;

    public float range;//矩形内随机，range为矩形边长的一半
    public float fishInterval;
    private FishDataList LoadedData;
    private Transform player;
    private List<List<FishData>> depthList = new List<List<FishData>>();

    private List<Transform> parents = new List<Transform>();
    public FishMove targetPrefab; //用于传递给pool

    private BoidSettings settings;

    private void Awake()
    {
        Instance = this;
    }


    public void Init()
    {
        settings = BoidsManager.Instance.settings;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        LoadedData = DataManager.Instance.LoadedData;
        InitDepthLists();
        //ShowLoadedData();
        SpawningAll();
        //StartCoroutine(DetectingPlayerDepth());
    }

    private IEnumerator DetectingPlayerDepth()
    {
        while (true)
        {
            float playerY = player.position.y;
            playerDepth = 0;
            for (; playerDepth < yDivide.Length - 1; playerDepth++)
            {
                if (playerY > yDivide[playerDepth])
                {
                    break;
                }
            }
            playerDepth = Mathf.Min(playerDepth, yDivide.Length - 2);
            Debug.Log("playerDepth "+playerDepth);
            for (int i=0;i<parents.Count;i++)
            {
                parents[i].gameObject.SetActive(false);
            }
            parents[playerDepth].gameObject.SetActive(true);
            if (playerDepth - 1 >= 0)
            {
                parents[playerDepth - 1].gameObject.SetActive(true);
            }
            if(playerDepth+1<parents.Count)
            {
                parents[playerDepth + 1].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(timeInterval);
        }
    }
    private IEnumerator MainSpawning()
    {
        while(true)
        {
            float playerY = player.position.y;
            playerDepth = 0;
            for(;playerDepth<yDivide.Length-1;playerDepth++)
            {
                if (playerY > yDivide[playerDepth])
                {
                    break;
                }
            }
            Debug.Log("playerDepth" + playerDepth);
            RandomSpawnFish(playerDepth, Vector3.zero);
            RandomSpawnFish(playerDepth - 1, Vector3.zero);
            RandomSpawnFish(playerDepth + 1, Vector3.zero);
            yield return new WaitForSeconds(timeInterval);
        }
    }
    private void SpawningAll()
    {
        for(int i=0;i<depthList.Count;i++)
        {
            for(int j=0;j<depthList[i].Count;j++)
            {
                int rarity= (int)depthList[i][j].rarity;
                int count = 6 - rarity;
                if (rarity == 4) { count = 1; }
                for(int k=0;k<count;k++)
                {
                    SpawnFish(i, j, Vector3.zero);
                }
            }
        }
    }

    public void ReSetting(FishMove fishMove)
    {
        Vector3 pos;
        Quaternion rot;
        int depth = fishMove.data.depth;

        RandomAreaXZ(out pos, out rot, range, Vector3.zero);
        pos += new Vector3(0,Random.Range(yDivide[depth], yDivide[depth + 1]), 0);
        fishMove.transform.position = pos;
        fishMove.transform.rotation = rot;
    }

    private void InitDepthLists()
    {
        //二维lists初始化
        for (int i = 0; i < yDivide.Length-1; i++)
        {
            depthList.Add(new List<FishData>());
            GameObject g = new GameObject(i.ToString());
            parents.Add(g.transform);
            //jellyDepthList.Add(new List<JellyData>());
        }
        //为每个list添加成员
        foreach (FishData data in LoadedData.datalist)
        {
            if (data.depth >= yDivide.Length - 1 || data.depth < 0)
            {
                Debug.LogError("depth error");
                data.depth = yDivide.Length - 2;
            }
            depthList[data.depth].Add(data);
        }
        /*
        foreach(JellyData data in LoadedData.jellyDatas)
        {
            if (data.depth >= yDivide.Length - 1 || data.depth < 0)
            {
                Debug.LogError("depth error");
                data.depth = yDivide.Length - 2;
            }
            jellyDepthList[data.depth].Add(data);
        }
        */
    }
    public void RandomSpawnFish(int depth, Vector3 centerPos)
    {
        Debug.Log(depth+" "+(yDivide.Length - 1));
        if(depth>= yDivide.Length - 1 || depth<0)
        {
            Debug.Log("depth out of range");
            return;
        }
        if (depthList[depth].Count == 0) { return; }
        int random = Random.Range(0, depthList[depth].Count);
        FishData data = depthList[depth][random];
        SpawnFish(data,Vector3.zero);
    }

    public void SpawnFish(int depth,int index,Vector3 center)
    {
        if (depth >= yDivide.Length - 1 || depth < 0)
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


    public void SpawnFish(FishData data, Vector3 center)
    {
        string fishname = data.name;
        FishMove fishPrefab = Resources.Load<FishMove>("Fish/" + fishname);
        if (fishPrefab == null)
        {
            Debug.Log(fishname + " prefab not exist!");
            return;
        }
        Vector3 pos;
        Quaternion rot;
        //RandomXZ(out pos, out rot, range, center);
        RandomAreaXZ(out pos, out rot, range, center);
        float downY = center.y + Random.Range(yDivide[data.depth], yDivide[data.depth + 1]);
        rot = Quaternion.LookRotation(new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)),Vector3.up);
        pos.y += downY;


        int fishNum = Random.Range(data.minPopulation, data.maxPopulation + 1);
        for (int i = 0; i < fishNum; i++)
        {
            FishMove fish = GameObject.Instantiate(fishPrefab, pos, rot);
            AllFishes.Add(fish);
            //不加入深度，若加入则去掉注释
            //fish.transform.parent = parents[data.depth];
            fish.data = data;
            fish.Init(settings);
            pos += new Vector3(Random.value, Random.Range(-1, 1), Random.Range(-1, 1)) * fishInterval;
        }
    }
    public void SpawnFishWithTarget(FishData data,Vector3 center)
    {
        string fishname = data.name;
        GameObject fishPrefab = Resources.Load<GameObject>("Fish/" + fishname);
        if (fishPrefab == null)
        {
            Debug.Log(fishname + " prefab not exist!");
            return;
        }

        Vector3 pos;
        Quaternion rot;
        //RandomXZ(out pos, out rot, range, center);
        RandomAreaXZ(out pos, out rot, range, center);
        pos += new Vector3(0, center.y + Random.Range(yDivide[data.depth], yDivide[data.depth + 1]), 0);

        //spawn target
        FishMove target = GameObject.Instantiate(targetPrefab);
        target.transform.position = pos;
        target.transform.rotation = rot;
        target.transform.parent = parents[data.depth];
        target.data = data;

        int fishNum = Random.Range(data.minPopulation, data.maxPopulation + 1);
        for (int i = 0; i < fishNum; i++)
        {
            GameObject fish = GameObject.Instantiate(fishPrefab,pos,Quaternion.identity);  //后续加入范围内根据target角度随机position,以及多条生成防止重合
                                                                                           //FishMove move=fish.AddComponent<FishMove>();
                                                                                           // move.data = data;
            fish.transform.parent = parents[data.depth];
            FollowFishMove follow = fish.GetComponent<FollowFishMove>();
            if (!follow)
            {
                follow=fish.AddComponent<FollowFishMove>();
                Debug.Log(fish.name + " no follow component!");
            }
            follow.target = target;
            pos += new Vector3(Random.value, Random.Range(-1, 1), Random.Range(-1, 1)) * fishInterval;
        }
    }

    public void RandomXZ(out Vector3 pos,out Quaternion rot,float range,Vector3 center)
    {
        range *= 1.4f;
       float x = Random.Range(0, range);
       float z = range - x; int r = Random.Range(0, 2);
        if (r == 0) { x *= -1; }
        r = Random.Range(0, 2);
        if (r == 0) { z *= -1; }
        pos = new Vector3(center.x + x,0, center.z + z);
        rot = Quaternion.LookRotation(new Vector3(-x, 0, -z), Vector3.up);
    }

    public void RandomAreaXZ(out Vector3 pos, out Quaternion rot, float range, Vector3 center)
    {
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);
        pos = new Vector3(center.x+x, 0, center.z+z);
        rot = Quaternion.LookRotation(new Vector3(-x, 0, -z), Vector3.up);
    }
    public void ShowLoadedData()
    {
        Debug.Log("loaded data length:" + LoadedData.datalist.Length);
        Debug.Log("jelly num:" + LoadedData.jellyDatas.Length);
        foreach (FishData data in LoadedData.datalist)
        {
            // Debug.Log(data.name);
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