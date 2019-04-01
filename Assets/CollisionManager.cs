using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public float ballMass;
    public Vector3 initialBallVelocity;
    public Vector3 finalBallVelocity;
    public float initialBallMomentum;
    public Vector3 finalBallMomentum;
    public float initialBallEnergy;
    public Vector3 finalBallEnergy;
    public float targetMass;
    public Vector3 initialTargetVelocity;
    public Vector3 finalTargetVelocity;
    public float initialTargetMomentum;
    public Vector3 finalTargetMomentum;
    public float initialTargetEnergy;
    public Vector3 finalTargetEnergy;
    public float e;
    public Vector3 j;
    public float jn;
    public float initialTotalMomentum;
    public Vector3 finalTotalMomentum;
    public float initialTotalEnergy;
    public Vector4 finalTotalEnergy;
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

        initialBallEnergy = Mathf.Abs(0.5f * ballMass * Mathf.Pow(initialBallVelocity.z, 2));
        initialTargetEnergy = Mathf.Abs(0.5f * targetMass * Mathf.Pow(initialTargetVelocity.z, 2));
        initialTotalEnergy = initialBallEnergy + initialTargetEnergy;

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
            initialTargetMomentum = target.velocity.z * targetMass;

            initialTotalMomentum = initialTargetMomentum + initialBallMomentum;
        }
        else if (hasCollided && !afterCollision)
        {
            n = (target.position - gunBall.position).normalized;
            Debug.Log(gunBall.position.z);
            Debug.Log(target.position.z);

            jn = Vector3.Dot(j, n);

            t = new Vector3(n.z, 0, n.x * -1);
            Debug.Log(t);

            float initialBallVelocityNormalized = Vector3.Dot(initialBallVelocity, n);

            float initialTargetVelocityNormalized = Vector3.Dot(initialTargetVelocity, n);

            float initialBallVelocityTangential = Vector3.Dot(initialBallVelocity, t);

            float initialTargetVelocityTangential = Vector3.Dot(initialTargetVelocity, t);

            Vector3 finalBallVelocityNormalized = (jn / ballMass + initialBallVelocityNormalized) * n;
            Vector3 finalTargetVelocityNormalized = (-jn / targetMass + initialTargetVelocityNormalized) * n;

            finalBallVelocity = finalBallVelocityNormalized + initialBallVelocityTangential * t;
            finalTargetVelocity = finalTargetVelocityNormalized + initialTargetVelocityTangential * t;

            gunBall.velocity = finalBallVelocity;
            target.velocity = finalTargetVelocity;

            finalBallMomentum = gunBall.velocity * ballMass;
            finalTargetMomentum = target.velocity * targetMass;

            finalTotalMomentum = finalTargetMomentum + finalBallMomentum;

            finalBallEnergy = 0.5f * ballMass * Vector3.Scale(gunBall.velocity, gunBall.velocity);
            finalTargetEnergy = 0.5f * targetMass * Vector3.Scale(target.velocity, target.velocity);

            finalTotalEnergy = finalBallEnergy + finalTargetEnergy;
            finalTotalEnergy.w = finalTotalEnergy.x + finalTotalEnergy.z;

            afterCollision = true;
        }
    }
}
