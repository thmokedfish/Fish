using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Fish")
        {
            Debug.Log("reset");
            FishSpawner.Instance.ReSetting(other.GetComponent<FishMove>());
        }
    }
}
