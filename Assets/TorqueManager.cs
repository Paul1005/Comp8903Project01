using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueManager : MonoBehaviour
{
    public float gunBallMass;
    public float targetMass;
    public float initialGunBallVelocity;
    public float initialTargetVelocity;
    public float finalGunBallVelocity;
    public float finalTargetVelocity;
    public float e;
    public float jn;
    public Vector3 P;
    public Vector3 R1;
    public Vector3 R2;
    public float initialGunBallMomentum;
    public float initialTargetMomentum;
    public float initialTotalMomentum;
    public float finalGunBallMomentum;
    public float finalTargetMomentum;
    public float finalTotalMomentum;
    public float initialGunBallKE;
    public float initialTargetKE;
    public float initialTotalKE;
    public Vector3 r1;
    public Vector3 r2;
    public float initialGunBallAngularMomentum;
    public float initialTargetAngularMomentum;
    public float initialTotalAngularMomentum;
    public float finalGunBallAngularMomentum;
    public float finalTargetAngularMomentum;
    public float finalTotalAngularMomentum;
    public float finalGunBallKE;
    public float finalTargetKE;
    public float finalTotalKE;
    public float finalGunBallRKE;
    public float finalTargetRKE;
    public float finalTotalRKE;
    public bool hasCollided;
    private bool afterCollision;

    private Vector3 n;
    public Vector3 vr;
    private Rigidbody gunBall;
    private Rigidbody target;

    // Use this for initialization
    void Start()
    {
        vr.z = initialGunBallVelocity - initialTargetVelocity;
        gunBall = GameObject.Find("Gunball").GetComponent<Rigidbody>();
        target = GameObject.Find("Target").GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCollided && !afterCollision)
        {
            gunBall.velocity = new Vector3(0, 0, initialGunBallVelocity);
            target.velocity = new Vector3(0, 0, initialTargetVelocity);

            initialGunBallMomentum = gunBall.velocity.z * gunBallMass;
            initialTargetMomentum = target.velocity.z * targetMass;

            initialTotalMomentum = initialTargetMomentum + initialGunBallMomentum;


        }
        else if (hasCollided && !afterCollision)
        {
            R1 = gunBall.position;
            R2 = target.position;
            n = (R2 - R1).normalized;

            P.z = R2.z - 0.5f;
            P.x = R1.x + (R2.x - R1.x) / 2;

            float i1 = 1 / 12 * gunBallMass * (1 + 1);
            float i2 = 1 / 12 * targetMass * (1 + 1);

            Vector3 j = new Vector3();
            j.z = -vr.z * (e + 1) * (1 / (1 / gunBallMass + 1 / targetMass + Vector3.Dot(n, Vector3.Scale(Vector3.Scale(r1, n) / i1, r1)) + Vector3.Dot(n, Vector3.Scale(Vector3.Scale(r2, n) / i2, r2))));
            jn = j.z;

            Vector3 finalGunBallVelocityNormalized = j.z * n / gunBallMass + initialGunBallVelocity * n;
            Vector3 finalTargetVelocityNormalized = j.z * n / targetMass + initialTargetVelocity * n;

            finalGunBallAngularMomentum = initialGunBallAngularMomentum + (Vector3.Scale(r1, j.z * n) / i1).z;
            finalTargetAngularMomentum = initialTargetAngularMomentum + (Vector3.Scale(r2, j.z * n) / i2).z;
        }
        
    }
}
