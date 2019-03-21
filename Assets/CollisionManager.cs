using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public float ballMass;
    public float initialBallVelocity;
    public float finalBallVelocity;
    public float initialBallMomentum;
    public float finalBallMomentum;
    public float targetMass;
    public float initialTargetVelocity;
    public float finalTargetVelocity;
    public float initialTargetMomentum;
    public float finalTargetMomentum;
    public float e;
    public float j;
    public float initialTotalMomentum;
    public float finalTotalMomentum;
    public float vr;
    public bool hasCollided;

    private Rigidbody gunBall;
    private Rigidbody target;

    // Use this for initialization
    void Start()
    {
        vr = initialBallVelocity - initialTargetVelocity;
        j = -vr * (e + 1) * (ballMass * targetMass) / (ballMass + targetMass);
        finalBallVelocity = j / ballMass + initialBallVelocity;
        finalTargetVelocity = -j / targetMass + initialTargetVelocity;

        gunBall = GameObject.Find("Gunball").GetComponent<Rigidbody>();
        target = GameObject.Find("Target").GetComponent<Rigidbody>();

        gunBall.mass = ballMass;
        target.mass = targetMass;

        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!hasCollided)
        {
            gunBall.velocity = new Vector3(0, 0, initialBallVelocity);
            target.velocity = new Vector3(0, 0, -initialTargetVelocity);

            initialBallMomentum = gunBall.velocity.z * ballMass;
            initialTargetMomentum = -target.velocity.z * targetMass;

            initialTotalMomentum = initialTargetMomentum + initialBallMomentum;
        }
        else if (hasCollided)
        {
            gunBall.velocity = new Vector3(0, 0, finalBallVelocity);
            target.velocity = new Vector3(0, 0, finalTargetVelocity);

            finalBallMomentum = gunBall.velocity.z * ballMass;
            finalTargetMomentum = target.velocity.z * targetMass;

            finalTotalMomentum = finalTargetMomentum + finalBallMomentum;
        }
    }
}
