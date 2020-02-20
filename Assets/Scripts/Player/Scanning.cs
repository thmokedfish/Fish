using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanning : MonoBehaviour
{
    public float ScanTime;
    private float _curValue;
    private float CurValue 
    {
        get 
        {
            return _curValue;
        }
        set 
        {
            _curValue = value;
            EventManager.Instance.InvokeValueChangeEvent("OnScanValueChange", _curValue/ScanTime);
        } 
    }
    public float maxDistance;

    public Material ScanningMaterial;
    private RaycastHit hit;
    private Transform currentFish;//正在扫描的鱼
    public SkinnedMeshRenderer currentRenderer;
    public Material currentMat;
    public OnValueChange OnScanValueChange;

    private void Start()
    {
    }
    private void Update()
    {
        Scan();
    }

    private void Scan()
    {

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(this.transform.position, transform.forward, out hit, maxDistance))
            {
                if (hit.transform.tag == "Fish")
                {
                    ScanFish(hit);
                    return;
                }
            }
        }
        OutOfScan();
    }
    private void ScanFish(RaycastHit hit)
    {
        if(hit.transform != currentFish)
        {
            currentFish = hit.transform;
            CurValue = 0;
            Transform child = hit.transform.Find("default");
            if (!child)
            {
                child = hit.transform.GetChild(0).Find("default");
            }
            if (currentRenderer)
            {
                currentRenderer.material = currentMat;
            }
            currentRenderer = child.GetComponent<SkinnedMeshRenderer>();
            currentMat = currentRenderer.material;
        }
        currentRenderer.material = ScanningMaterial;
        CurValue += Time.deltaTime;
        if (CurValue > ScanTime)
        {
            CurValue = 0;
            FinishScan(hit);
        }
        
    }

    private void OutOfScan()
    {
        if (currentRenderer)
        {
            currentRenderer.material = currentMat;
        }
        if (CurValue > 0)
        {
            CurValue -= Time.deltaTime/2;
        }
        else
            CurValue = 0;
    }

    private void FinishScan(RaycastHit hit)
    {
        FollowFishMove follow=hit.transform.GetComponent<FollowFishMove>();
        if(!follow)
        {
            Debug.LogError("no fishmove!");
            return;
        }
        FishData fish = follow.target.data;
        Debug.Log(fish.info);
        EventManager.Instance.InvokeReferenceEvents("OnScanFinish", fish);
    }
}
