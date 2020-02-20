using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [Serializable]
    public class ShowData
    {
        public string name;
        public string info;
        public Rarity rarity;
        public int depth;
        public float moveRate;
        public float shakeFrequency = 0;
        public float shakeAmplitude = 0;
        public float rotateRange = 45;
        public float rotateInterval = 2.5f;
        public int minPopulation = 1;
        public int maxPopulation = 1;
        public string briefInfo;
        public int ID;
    }
    [Serializable]
    public class DataList
    {
        public List<ShowData> datalist;
    }

    GameObject[] fish = new GameObject[28];
    Button[] fishButton = new Button[28];
    ShowData[] fishData = new ShowData[28];

    public GameObject exhibitionStand;
    public GameObject buttonParent;
    public Button buttonPrefab;
    public Button returnButton;

    GameObject temp;

    DataList dl = new DataList();
    public int[] fishNumber;

    void Start()
    {
        InitFish();
        LoadJson();
        FishNumberInit();
        FishSet(0);
        InitButton();
    }
    void Change(int i)
    {
        if (temp != null)
        {
            GameObject.Destroy(temp);
        }
        FishSet(i);
    }
    void InitFish()
    {
        fish[0] = Resources.Load<GameObject>("Exhibition/Prefabs/butterflyfish");
        fish[1] = Resources.Load<GameObject>("Exhibition/Prefabs/carp");
        fish[2] = Resources.Load<GameObject>("Exhibition/Prefabs/discus");
        fish[3] = Resources.Load<GameObject>("Exhibition/Prefabs/electric_catfish");
        fish[4] = Resources.Load<GameObject>("Exhibition/Prefabs/goliath_grouper");
        fish[5] = Resources.Load<GameObject>("Exhibition/Prefabs/green_turtle");
        fish[6] = Resources.Load<GameObject>("Exhibition/Prefabs/lionfish");
        fish[7] = Resources.Load<GameObject>("Exhibition/Prefabs/longear_sunfish");
        fish[8] = Resources.Load<GameObject>("Exhibition/Prefabs/losejaw");
        fish[9] = Resources.Load<GameObject>("Exhibition/Prefabs/moorish_idol");
        fish[10] = Resources.Load<GameObject>("Exhibition/Prefabs/nimbochromis");
        fish[11] = Resources.Load<GameObject>("Exhibition/Prefabs/opthalmosaurus");
        fish[12] = Resources.Load<GameObject>("Exhibition/Prefabs/oreochromis");
        fish[13] = Resources.Load<GameObject>("Exhibition/Prefabs/peacock_bass");
        fish[14] = Resources.Load<GameObject>("Exhibition/Prefabs/pike");
        fish[15] = Resources.Load<GameObject>("Exhibition/Prefabs/rainbow_cichlid");
        fish[16] = Resources.Load<GameObject>("Exhibition/Prefabs/red_rockfish");
        fish[17] = Resources.Load<GameObject>("Exhibition/Prefabs/rockfish");
        fish[18] = Resources.Load<GameObject>("Exhibition/Prefabs/shark");
        fish[19] = Resources.Load<GameObject>("Exhibition/Prefabs/stingray");
        fish[20] = Resources.Load<GameObject>("Exhibition/Prefabs/synodontis_eupterus");
        fish[21] = Resources.Load<GameObject>("Exhibition/Prefabs/yellow_snapper");
        fish[22] = Resources.Load<GameObject>("Exhibition/Prefabs/cuttlefish");
        fish[23] = Resources.Load<GameObject>("Exhibition/Prefabs/dumbo_octopus");
        fish[24] = Resources.Load<GameObject>("Exhibition/Prefabs/giant_squid");
        fish[25] = Resources.Load<GameObject>("Exhibition/Prefabs/jellyfish");
        fish[26] = Resources.Load<GameObject>("Exhibition/Prefabs/sea_urchin");
        fish[27] = Resources.Load<GameObject>("Exhibition/Prefabs/starfish");
    }
    void InitButton()
    {
        for (int i = 0; i < 28; i++)
        {
            Button go = GameObject.Instantiate(buttonPrefab);
            go.transform.SetParent(buttonParent.transform);
            if (fishNumber[i] == 1)
            {
                go.GetComponentInChildren<Text>().text = "    " + fishData[i].ID.ToString() + "  " + fishData[i].briefInfo;
            }
            else
            {
                go.GetComponentInChildren<Text>().text = "    " + fishData[i].ID.ToString() + "  " + "??????";
            }
            int j = i;
            go.onClick.AddListener(() => Change(j));
            fishButton[i] = go;
        }
        returnButton.onClick.AddListener(delegate () { SwitchScene(1); });
    }
    public void FishSet(int i)
    {
        if (fishNumber[i] == 0) return;
        temp = GameObject.Instantiate(fish[i], new Vector3(0, 2.5f, 0), Quaternion.identity);
        temp.GetComponent<DataNumber>().data = fishData[i];
        temp.transform.SetParent(exhibitionStand.transform);
    }
    public void LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "FishData/ShowData.json");
        string json = File.ReadAllText(filePath);
        dl = JsonUtility.FromJson<DataList>(json);
        int i = 0;
        foreach (ShowData sd in dl.datalist)
        {
            fishData[i] = sd;
            i++;
        }
        QuickSortRelax(fishData, 0, fishData.Length-1);
    }
    public static void QuickSortRelax(ShowData[] a, int low, int high)
    {
        if (low >= high) return;
        int temp = a[(low + high) / 2].ID;
        int i = low - 1, j = high + 1;
        while (true)
        {
            while (a[++i].ID < temp) ;
            while (a[--j].ID > temp) ;
            if (i >= j) break;
            ShowData temp1 = a[i];
            a[i] = a[j];
            a[j] = temp1;
        }
        QuickSortRelax(a, j + 1, high);
        QuickSortRelax(a, low, i - 1);
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void FishNumberInit()
    {
        fishNumber = new int[28];
        foreach (string name in DataManager.Instance.capturedNameSet)
        {
            for (int i = 0; i < 28; i++)
            {
                if (fishData[i].name == name)
                {
                    Debug.Log(name);
                    if (fishNumber[i] == 0)
                    {
                        fishNumber[i] = 1;
                    }
                }
            }
        }
    }
}
