using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueManager : MonoBehaviour
{
    public float gunBallMass;
    public float targetMass;
    public Vector3 initialGunBallVelocity;
    public Vector3 initialTargetVelocity;
    public Vector3 finalGunBallVelocity;
    public Vector3 finalTargetVelocity;
    public float e;
    public float jn;
    public Vector3 P;
    public Vector3 R1;
    public Vector3 R2;
    public float initialGunBallMomentum;
    public float initialTargetMomentum;
    public float initialTotalMomentum;
    public Vector3 finalGunBallMomentum;
    public Vector3 finalTargetMomentum;
    public Vector3 finalTotalMomentum;
    public float initialGunBallKE;
    public float initialTargetKE;
    public float initialTotalKE;
    public Vector3 r1;
    public Vector3 r2;
    public float initialGunBallAngularMomentum;
    public float initialTargetAngularMomentum;
    public float initialGunBallAngularVelocity;
    public float initialTargetAngularVelocity;
    public float initialTotalAngularMomentum;
    public Vector3 finalGunBallAngularMomentum;
    public Vector3 finalTargetAngularMomentum;
    public Vector3 finalTotalAngularMomentum;
    public float finalGunBallAngularVelocity;
    public float finalTargetAngularVelocity;
    public Vector3 finalGunBallKE;
    public Vector3 finalTargetKE;
    public Vector3 finalTotalKE;
    public Vector3 finalGunBallRKE;
    public Vector3 finalTargetRKE;
    public Vector3 finalTotalRKE;
    public Vector3 finalTotalKERKE;
    public bool hasCollided;
    private bool afterCollision;

    public Vector3 n;
    public Vector3 vr;
    private Rigidbody gunBall;
    private Rigidbody target;
    public float i1;
    public float i2;
    public Vector3 t;

    // Use this for initialization
    void Start()
    {
        vr = initialGunBallVelocity - initialTargetVelocity;
        gunBall = GameObject.Find("Gunball").GetComponent<Rigidbody>();
        target = GameObject.Find("Target").GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCollided && !afterCollision)
        {
            gunBall.velocity = initialGunBallVelocity;
            target.velocity = initialTargetVelocity;

            initialGunBallMomentum = gunBall.velocity.z * gunBallMass;
            initialTargetMomentum = target.velocity.z * targetMass;

            initialTotalMomentum = initialTargetMomentum + initialGunBallMomentum;

            initialGunBallKE = 0.5f * gunBall.velocity.z * gunBall.velocity.z * gunBallMass;
            initialTargetKE = 0.5f * target.velocity.z * target.velocity.z * targetMass;

            initialTotalKE = initialGunBallKE + initialTargetKE;
        }
        else if (hasCollided && !afterCollision)
        {
            R1 = gunBall.position;
            R2 = target.position;
            n = new Vector3(0, 0, 1.0f);
            t = new Vector3(1.0f, 0, 0);

            P.z = R2.z - 0.5f;
            P.x = R1.x + (R2.x - R1.x) / 2.0f;

            r1 = P - R1;
            r2 = P - R2;

            i1 = 1.0f / 12.0f * gunBallMass * (1.0f + 1.0f);
            i2 = 1.0f / 12.0f * targetMass * (1.0f + 1.0f);

            Vector3 j = new Vector3();
            j.z = -vr.z * (e + 1.0f) * (1.0f / (1.0f / gunBallMass + 1.0f / targetMass
                + Vector3.Dot(n, Vector3.Cross(Vector3.Cross(r1, n) / i1, r1))
                + Vector3.Dot(n, Vector3.Cross(Vector3.Cross(r2, n) / i2, r2))));
            jn = j.z;

            finalGunBallVelocity.z = j.z / gunBallMass + initialGunBallVelocity.z * n.z;
            finalGunBallVelocity.x = j.z / gunBallMass + initialGunBallVelocity.x * n.x;
            finalTargetVelocity.z = -j.z / targetMass + initialTargetVelocity.z * n.z;
            finalTargetVelocity.x = -j.z / targetMass + initialTargetVelocity.x * n.x;

            gunBall.velocity = finalGunBallVelocity;
            target.velocity = finalTargetVelocity;

            finalGunBallMomentum = finalGunBallVelocity * gunBallMass;
            finalTargetMomentum = finalTargetVelocity * targetMass;
            finalTotalMomentum = finalGunBallMomentum + finalTargetMomentum;

            finalGunBallKE = 0.5f * Vector3.Scale(finalGunBallVelocity, finalGunBallVelocity) * gunBallMass;
            finalTargetKE = 0.5f * Vector3.Scale(finalTargetVelocity, finalTargetVelocity) * targetMass;
            finalTotalKE = finalGunBallKE + finalTargetKE;

            finalGunBallAngularVelocity = initialGunBallAngularVelocity + (Vector3.Cross(r1, -j.z * n) / i1).y;
            finalTargetAngularVelocity = initialTargetAngularVelocity + (Vector3.Cross(r2, j.z * n) / i2).y;

            finalGunBallAngularMomentum = new Vector3(0, finalGunBallAngularVelocity * i1, 0);
            finalTargetAngularMomentum = new Vector3(0, finalTargetAngularVelocity * i2, 0);
            finalTotalAngularMomentum = finalGunBallAngularMomentum + finalTargetAngularMomentum;

            finalGunBallRKE = 0.5f * i1 * Vector3.Scale(new Vector3(0, finalGunBallAngularVelocity, 0), new Vector3(0, finalGunBallAngularVelocity, 0));
            finalTargetRKE = 0.5f * i2 * Vector3.Scale(new Vector3(0, finalTargetAngularVelocity, 0), new Vector3(0, finalTargetAngularVelocity, 0));
            finalTotalRKE = finalGunBallRKE + finalTargetRKE;

            finalTotalKERKE = finalTotalKE + finalTotalRKE;
            afterCollision = true;
        }

        gunBall.transform.Rotate(0, -finalGunBallAngularVelocity * Time.deltaTime * Mathf.Rad2Deg, 0);
        target.transform.Rotate(0, -finalTargetAngularVelocity * Time.deltaTime * Mathf.Rad2Deg, 0);
    }
}
