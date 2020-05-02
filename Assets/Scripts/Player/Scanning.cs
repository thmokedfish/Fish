﻿using System.Collections;
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

    private bool isScanning = false;

    
    public Material[] tempMaterial=new Material[1];
    public Material[] scanMaterial=new Material[1];
    int isDoing = 0;
    int over = 1;

    private void Start()
    {
    }
    private void Update()
    {
        Scan();
    }

    private void Scan()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isScanning = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isScanning = false;
        }

        if(isScanning)
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
        if (hit.transform != fish)
        {
            if (fish)
            {
                fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = tempMaterial;
            }
            fish = hit.transform;
            CurValue = 0;
            tempMaterial = fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            isDoing = 1;
        }
        if (hit.transform==fish&&isDoing==1)
        {
            fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = scanMaterial;
            over =0;
        }
        CurValue += Time.deltaTime;
        if (CurValue > ScanTime)
        {
            CurValue = 0;
            isScanning = false;
            FinishScan(hit);
        }
    }

    private void OutOfScan()
    {
        if (CurValue > 0)
        {
            CurValue -= Time.deltaTime/2;
        }
        else
            CurValue = 0;
        if (over ==0)
        {
            fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = tempMaterial;
        }
    }

    private void FinishScan(RaycastHit hit)
    {
        FishMove follow=hit.transform.GetComponent<FishMove>();
        if(!follow)
        {
            Debug.LogError("no fishmove!");
            return;
        }
        fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = tempMaterial;
        isDoing = 0;
        over = 1;
        FishData fishData = follow.data;
        Debug.Log(fishData.info);
        EventManager.Instance.InvokeReferenceEvents("OnScanFinish", fishData);
        fish = null;
    }
}
