using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private RaycastHit hit;
    private Transform fish;
    public Material[] tempMaterial = new Material[1];
    public Material[] scanMaterial=new Material[1];
    void Start()
    {
        fish = null; 
    }

    // Update is called once per frame
    void Update()
    {
        
        Scan();
    }
    private void Scan()
    {

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(this.transform.position, transform.forward, out hit))
            {
                if (hit.transform.tag == "Fish")
                {
                    if (hit.transform != fish)
                    {
                        fish = hit.transform;
                        tempMaterial = fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;
                        Debug.Log(tempMaterial[0]);
                        fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = scanMaterial;
                        Debug.Log(tempMaterial[0]);
                        Debug.Log(fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0]);
                        return;
                    }
                }
            }
        }
       OutOfScan();
    }
    private void OutOfScan()
    {
        if (fish != null)
        {
            Debug.Log(tempMaterial[0]);
            fish.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials= tempMaterial;
            Debug.Log(tempMaterial[0]);
        }
    }
}
