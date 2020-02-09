using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFishMove : MonoBehaviour
{
    public FishMove target;
    void Update()
    {
        if(!target)
        {
            Debug.Log("target is null");
            Destroy(this.gameObject);
        }
        if(!target.gameObject.activeSelf)//target被回收
        {
            Destroy(this.gameObject);
        }
        if(target.transform)
        transform.position += target.PositionDiffPerFrame;
        transform.rotation = target.transform.rotation;
    }
}
