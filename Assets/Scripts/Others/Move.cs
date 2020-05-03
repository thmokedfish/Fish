using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed=1;

    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        if (H != 0 || V != 0)
        {
            this.transform.Translate(new Vector3(H, 0, V) * Time.deltaTime * moveSpeed, Space.Self);
        }
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                if (Input.GetAxis("Mouse X") < 0.1f && Input.GetAxis("Mouse X") > -0.1f)
                {
                    return;
                }
                this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.fixedDeltaTime * 200, 0));
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {
                if (Input.GetAxis("Mouse Y") < 0.1f && Input.GetAxis("Mouse Y") > -0.1f)
                {
                    return;
                }
                this.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * -200, 0, 0));
            }
        }
    }
}
