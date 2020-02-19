using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public Transform target;//获取旋转目标

    private float mouse_x;

    private float mouse_y;

    private void LateUpdate() //摄像机围绕目标旋转操作
    {
        float mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴增量
        Debug.Log(mouse_x);
        float mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴增量

        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, Vector3.up, mouse_x * 5);
            transform.RotateAround(target.transform.position, transform.right, mouse_y * 5);
        }
    }  
}
