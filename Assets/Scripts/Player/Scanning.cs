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
    private RaycastHit hit;
    private Transform fish;
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
        if(hit.transform != fish)
        {
            fish = hit.transform;
            CurValue = 0;
        }
        CurValue += Time.deltaTime;
        if (CurValue > ScanTime)
        {
            CurValue = 0;
            FinishScan(hit);
        }
    }

    private void OutOfScan()
    {
        if (CurValue > 0)
        {
            CurValue -= Time.deltaTime;
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
