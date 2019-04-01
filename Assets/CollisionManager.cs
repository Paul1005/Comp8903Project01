using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public float ballMass;
    public Vector3 initialBallVelocity;
    public Vector3 finalBallVelocity;
    public float initialBallMomentum;
    public float finalBallMomentum;
    public float initialBallEnergy;
    public float finalBallEnergy;
    public float targetMass;
    public Vector3 initialTargetVelocity;
    public Vector3 finalTargetVelocity;
    public float initialTargetMomentum;
    public float finalTargetMomentum;
    public float initialTargetEnergy;
    public float finalTargetEnergy;
    public float e;
    public Vector3 j;
    public float jn;
    public float initialTotalMomentum;
    public float finalTotalMomentum;
    public float initialTotalEnergy;
    public float finalTotalEnergy;
    public Vector3 vr;
    public Vector3 n;
    public bool hasCollided;
    private bool afterCollision;
    private Vector3 t;
    private Rigidbody gunBall;
    private Rigidbody target;

    // Use this for initialization
    void Start()
    {
        vr = initialBallVelocity - initialTargetVelocity;
        j.z = -vr.z * (e + 1) * (ballMass * targetMass) / (ballMass + targetMass);

        gunBall = GameObject.Find("Gunball").GetComponent<Rigidbody>();
        target = GameObject.Find("Target").GetComponent<Rigidbody>();

        gunBall.mass = ballMass;
        target.mass = targetMass;

        hasCollided = false;
        afterCollision = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!hasCollided && !afterCollision)
        {
            gunBall.velocity = initialBallVelocity;
            target.velocity = initialTargetVelocity;

            initialBallMomentum = gunBall.velocity.z * ballMass;
            initialTargetMomentum = -target.velocity.z * targetMass;

            initialTotalMomentum = initialTargetMomentum + initialBallMomentum;
        }
        else if (hasCollided && !afterCollision)
        {
            n = (target.position - gunBall.position).normalized;

            jn = Vector3.Dot(j, n);

            t = new Vector3(n.z, 0, n.x * -1);
            Debug.Log(t);
            float initialBallVelocityNormalized = Vector3.Dot(initialBallVelocity, n);
            Debug.Log(initialBallVelocityNormalized);
            float initialTargetVelocityNormalized = Vector3.Dot(initialTargetVelocity, n);
            Debug.Log(initialTargetVelocityNormalized);
            float initialBallVelocityTangential = Vector3.Dot(initialBallVelocity, t);
            Debug.Log(initialBallVelocityTangential);
            float initialTargetVelocityTangential = Vector3.Dot(initialTargetVelocity, t);
            Debug.Log(initialTargetVelocityTangential);

            Vector3 finalBallVelocityNormalized = (jn / ballMass + initialBallVelocityNormalized) * n;
            Vector3 finalTargetVelocityNormalized = (-jn / targetMass + initialTargetVelocityNormalized) * n;

            finalBallVelocity = finalBallVelocityNormalized + initialBallVelocityTangential * t;
            finalTargetVelocity = finalTargetVelocityNormalized + initialTargetVelocityTangential * t;

            gunBall.velocity = finalBallVelocity;
            target.velocity = finalTargetVelocity;

            finalBallMomentum = gunBall.velocity.z * ballMass;
            finalTargetMomentum = target.velocity.z * targetMass;

            finalTotalMomentum = finalTargetMomentum + finalBallMomentum;

            afterCollision = true;
        }
    }
}
