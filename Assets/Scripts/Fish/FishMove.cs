using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishMove : MonoBehaviour
{
    public BoidSettings settings;
    public FishData data;

    public Vector3 PositionDiffPerFrame { get; private set; }

    private Vector3 lastpos;
    private float lastX = 0;
    private float shakeAngle = 0;

    [HideInInspector] public Vector3 position;
    [HideInInspector]public Vector3 forward { get; set; }

    private Vector3 velocity;
    private Vector3 acceleration;
    private Transform myTransform;
    Vector3 target;
    float startY;

    [HideInInspector] public Vector3 avgFlockHeading;
    [HideInInspector] public Vector3 avgAvoidanceHeading;
    [HideInInspector] public Vector3 centreOfFlockmates;
    [HideInInspector] public int numPerceivedFlockmates;

    public void Init(BoidSettings settings)
    {
        this.settings = settings;
        myTransform = transform;
        position = myTransform.position;
        startY = position.y;
        target = position;
        forward = myTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }
    public void OnUpdate()
    {
        Vector3 acceleration = Vector3.zero;

      // target = new Vector3(position.x, startY, position.z);
      //  Vector3 offsetToTarget = target - position;
      //  acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        

        if (numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }
        acceleration.y = 0;
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        myTransform.position += velocity * Time.deltaTime;
       // Quaternion.Lerp()
        myTransform.forward = dir;

        position = myTransform.position;
        forward = dir;
    }
    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask))
        {
            return true;
        }
        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = myTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }

        return -forward;
    }


    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }
    


    private void CalculatePositionDiff()
    {
        PositionDiffPerFrame = transform.position - lastpos;
        lastpos = transform.position;
    }
    /*
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
    }*/
}

