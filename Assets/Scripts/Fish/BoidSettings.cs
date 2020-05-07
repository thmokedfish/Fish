using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSettings:MonoBehaviour
{
    // Settings
    public float minSpeed = 5;
    public float maxSpeed = 15;
    public float perceptionRadius = 25f;
    public float avoidanceRadius = 20;
    public float maxSteerForce = 3;

    public float alignWeight = 10;
    public float cohesionWeight = 10;
    public float seperateWeight = 10;

    public float targetWeight = 1;

    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = 1f;
    public float avoidCollisionWeight = 150;
    public float collisionAvoidDst = 50;

}