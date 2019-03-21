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

    private Rigidbody gunBall;
    private Rigidbody target;

    // Use this for initialization
    void Start()
    {
        vr = initialBallVelocity - initialTargetVelocity;
        j = -vr * (e + 1) * (ballMass * targetMass) / (ballMass + targetMass);
        finalBallVelocity = j / ballMass + initialBallVelocity;
        finalTargetVelocity = -j / targetMass + initialTargetVelocity;

        initialBallMomentum = initialBallVelocity * ballMass;
        initialTargetMomentum = initialTargetVelocity * targetMass;
        finalBallMomentum = finalBallVelocity * ballMass;
        finalTargetMomentum = finalTargetVelocity * targetMass;

        initialTotalMomentum = initialTargetMomentum + initialBallMomentum;
        finalTotalMomentum = finalTargetMomentum + finalBallMomentum;

        gunBall = GameObject.Find("Gunball").GetComponent<Rigidbody>();
        target = GameObject.Find("Target").GetComponent<Rigidbody>();

        gunBall.velocity = new Vector3(0, 0, initialBallVelocity);
        target.velocity = new Vector3(0, 0, -initialTargetVelocity);

        gunBall.mass = ballMass;
        target.mass = targetMass;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}
