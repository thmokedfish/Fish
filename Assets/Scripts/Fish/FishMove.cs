using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishMove : MonoBehaviour
{
    public FishData data;

    public Vector3 PositionDiffPerFrame { get; private set; }

    private Vector3 lastpos;
    private float lastX = 0;
    private float shakeAngle = 0;

    private void Start()
    {
        lastpos = transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine(Rotate(data.rotateRange, data.rotateInterval));
    }
    private void Update()
    {
        Move();
        CalculatePositionDiff();
    }
    //到达边界despawn

    private void CalculatePositionDiff()
    {
        PositionDiffPerFrame = transform.position - lastpos;
        lastpos = transform.position;
    }
    private void Move()
    {
        float x = Mathf.Cos(shakeAngle * Mathf.Deg2Rad) * data.shakeAmplitude;
        transform.Translate(x - lastX, 0, Time.deltaTime * data.moveRate, Space.Self);
        lastX = x;
        shakeAngle += data.shakeFrequency * Time.deltaTime;
        if (shakeAngle > 360)
        {
            shakeAngle -= 360;
        }
    }
    private IEnumerator Rotate(float maxAngle, float interval)
    {
        while(true)
        {
            // Quaternion randomQuaternionInRange = Quaternion.AngleAxis(Random.Range(0, maxAngle), transform.up);
            //Quaternion rotationInPlane = transform.rotation * randomQuaternionInRange;
            //Quaternion result = rotationInPlane * Quaternion.AngleAxis(Random.Range(0, 360), transform.forward);
            // transform.DORotateQuaternion(result, 1f);
            Vector3 randomDir = Quaternion.AngleAxis(Random.Range(0, maxAngle), transform.up) * transform.forward;
            Vector3 result = Quaternion.AngleAxis(Random.Range(0, 360), transform.forward) * randomDir;
            result.y = result.y / 3;
            transform.DORotateQuaternion(Quaternion.LookRotation(result, Vector3.up), interval);
            yield return new WaitForSeconds(interval);
        }
    }
}
