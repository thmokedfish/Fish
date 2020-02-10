using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanning : MonoBehaviour
{
    public float ScanTime;
    private float curValue;
    private RaycastHit hit;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Physics.Raycast(this.transform.position, transform.forward, out hit))
            {
                OnScan(hit);
            }
            else
            {
                OutOfScan();//value drop
            }

        }
    }
    private void OnScan(RaycastHit hit)
    {
        curValue += Time.deltaTime;
        if (curValue > ScanTime)
        {
            curValue = 0;
            Scan();
        }
    }

    private void OutOfScan()
    {
        curValue -= Time.deltaTime;
    }

    private void Scan()
    {
    }
}
