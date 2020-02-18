using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    ScrollRect scrollRect;
    public GameObject exhibition;
    GameObject per;
    GameObject next;

    public Button b1;
    void Start()
    {
        b1.onClick.AddListener(Change);
    }
    private void Update()
    {
        
    }
    void Change()
    {
        next = Resources.Load<GameObject>("Exhibition/Prefabs/butterflyfish");
        GameObject go= GameObject.Instantiate(next, new Vector3(0, 2.3f, 0), Quaternion.identity);
        go.transform.SetParent(exhibition.transform);
    }
}
