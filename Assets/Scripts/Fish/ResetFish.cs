using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="FishTarget")
        {
            FishSpawner.Instance.ReSetting(other.GetComponent<FishMove>());
        }
    }
}
