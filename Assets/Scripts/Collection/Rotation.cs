using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 35;
    private void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, speed*1 * Time.deltaTime, 0));
    }
}
