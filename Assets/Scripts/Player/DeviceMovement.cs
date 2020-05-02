using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeviceMovement : MonoBehaviour
{
    Gyroscope m_Gyro;
    Vector3 velocity;
    Rigidbody m_Rigidbody;
    public bool rbMood;
    public float speed = 10;
    public float minVelocity = 0.05f;
    private bool OnUI = false;
    private void Start()
    {
        velocity = Vector3.zero;
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        m_Rigidbody = this.GetComponent<Rigidbody>();
        if(!m_Rigidbody)
        {
            rbMood = false;
        }

        EventManager.Instance.AddValueChangeEvent("Sensitivity", ChangeSensitivity);
        EventManager.Instance.AddValueChangeEvent("CursorHide", HideCursor);
        rb = this.GetComponent<Rigidbody>();
    }
    private Rigidbody rb;
    void Update()
    {
        //CheckMoveFromInput();

        if (!OnUI)
        {
            CheckRotate();
        }
        else
        {
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 velocity= transform.right * h + transform.forward * v ;
        velocity = velocity.normalized * Mathf.Max(Mathf.Abs(h), Mathf.Abs(v)) * speed;
        velocity += Vector3.up * Input.GetAxis("Jump") * speed;
        rb.velocity = velocity;
            //new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
    }

    private void CheckMoveFromInput()
    {
        /*
        if (Input.touchCount > 0)
        {
            this.transform.Translate(0, 0, Input.touches[0].deltaPosition.y*Time.deltaTime*speed,Space.Self);
            return;
        }
        */

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical")  * speed)*Time.deltaTime;
        this.transform.Translate(dir, Space.Self);
    }


    private void CheckMove()
    {
        if (rbMood)
        {
            RigidbodyMovement();
        }
        else
        {
            CalculateMovement();
        }
    }
    [Header("PC_Rotate")]
    public float sensitivityHor = 9.0f;//水平旋转的速度
    public float sensitivityVert = 9.0f;//垂直旋转的速度
    public float minimumVert = -45.0f;//垂直旋转的最小角度
    public float maximumVert = 45.0f;//垂直旋转的最小角度
    private float _rotationX = 0;//为垂直角度声明一个私有变量
    private float _rotationY = 0;//为垂直角度声明一个私有变量

    private void HideCursor(float val)
    {
        Debug.Log("hidecursor " + val);
        if(val>0)
        {
            OnUI = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            OnUI = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    private void ChangeSensitivity(float sense)
    {
        sensitivityHor = 6*sense;
        sensitivityVert =6*sense;
    }
    private void CheckRotate()
    {
        
//#if UNITY_EDITOR
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);//限制角度大小
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;//设置水平旋转的变化量
        _rotationY =_rotationY + delta;//原来的角度加上变化量
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);//相对于全局坐标空间的角度
        return;
//#endif
/*
#if UNITY_ANDROID
        if(!transform.parent)
        {
            InitParent();
        }
        Quaternion q = GyroToUnity(m_Gyro.attitude);
        transform.localRotation = Quaternion.Lerp(transform.localRotation,q, 0.5f);
#endif

*/
    }

    private void InitParent()
    {
        GameObject parent = new GameObject("CameraParent");
        parent.transform.position = transform.position;
        transform.parent = parent.transform;
        parent.transform.eulerAngles = new Vector3(90, 0, 0);
    }
    private void RigidbodyMovement()
    {
        if(!m_Rigidbody)
        {
            m_Rigidbody = this.GetComponent<Rigidbody>();
        }
        Vector3 dir = new Vector3(-m_Gyro.userAcceleration.x, -m_Gyro.userAcceleration.y, m_Gyro.userAcceleration.z);
        m_Rigidbody.AddForce(dir);
    }
    private void CalculateMovement()
    {
        Vector3 dir = new Vector3(-m_Gyro.userAcceleration.x, -m_Gyro.userAcceleration.y, m_Gyro.userAcceleration.z);
        Vector3 acc = dir ;
        velocity += acc * speed * Time.deltaTime; //游戏速度，有误差
        //if (dir.sqrMagnitude < 0.001)
        {
           // velocity = Vector3.zero;
        }
        transform.Translate(velocity);
    }
    /*
    private void OnGUI()
    {
        // GUI.Label(new Rect(500, 450, 400, 80), "input Acceleration:" + Input.acceleration);
        // GUI.Label(new Rect(500, 400, 400, 80), "gyro Acceleration:" + m_Gyro.userAcceleration);
#if UNITY_EDITOR
        GUI.Label(new Rect(500, 450, 400, 80), "editor");
#endif

#if UNITY_ANDROID
        GUI.Label(new Rect(500, 400, 400, 80), "android");
#endif
    }
    */
    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z,- q.w);
    }


}
