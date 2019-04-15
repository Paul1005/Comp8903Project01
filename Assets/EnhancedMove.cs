using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedMove : MonoBehaviour
{
    public Vector3 dinZ;
    private float forceX;
    private float forceZ;
    private float angularForce;
    public float hullMass;
    public float gunMass;
    public float pilotMass;
    private float time;
    float positionX;
    float positionZ;
    private float comMass;
    private Vector3 acceleration;
    private Vector3 velocity;

    public Vector3 dFinalLeft;
    public Vector3 dFinalRight;
    public float momentOfInertia;

    public float tFinalLeft;
    public float tFinalRight;
    public Vector3 rLeft;
    public Vector3 rRight;
    public Vector3 torqueLeft;
    public Vector3 torqueRight;
    public Vector3 accLeft;
    public Vector3 accRight;
    public Vector3 thrust;
    public Vector3 comPosition;
    private float rotationaVelocity;
    public GameObject hull;
    public GameObject gun;
    public GameObject pilot;

    // Use this for initialization
    void Start()
    {
        comMass = hullMass + gunMass + pilotMass;
        positionX = gameObject.transform.position.x;
        positionZ = gameObject.transform.position.z;

        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");

        float lyHull = hullMass * (Mathf.Pow(hull.transform.localScale.x, 2) + Mathf.Pow(hull.transform.localScale.z, 2)) / 12;
        float h2Hull = Mathf.Pow(comPosition.x - hull.transform.position.x, 2) + Mathf.Pow(comPosition.z - hull.transform.position.z, 2);
        float mh2Hull = h2Hull * hullMass;
        float inertiaHull = lyHull + mh2Hull;

        float lyPilot = hullMass * (Mathf.Pow(pilot.transform.localScale.x, 2) + Mathf.Pow(pilot.transform.localScale.z, 2)) / 12;
        float h2Pilot = Mathf.Pow(comPosition.x - pilot.transform.position.x, 2) + Mathf.Pow(comPosition.z - pilot.transform.position.z, 2);
        float mh2Pilot = h2Pilot * pilotMass;
        float inertiaPilot = lyPilot + mh2Pilot;

        momentOfInertia = inertiaHull + inertiaPilot;

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forceZ = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forceZ = -1;
        }
        else
        {
            forceZ = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            forceX = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            forceX = -1;
        }
        else
        {
            forceX = 0;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            angularForce = -1;
        } else if (Input.GetKey(KeyCode.E))
        {
            angularForce = 1;
        }
        else
        {
            angularForce = 0;
        }

        Vector3 thrust = new Vector3(forceX * 10000.0f, 0, forceZ * 10000.0f);
        acceleration = new Vector3(thrust.x / comMass, 0, thrust.z / comMass);

        positionX += velocity.x * Time.deltaTime + 0.5f * acceleration.x * Mathf.Pow(Time.deltaTime, 2);
        positionZ += velocity.z * Time.deltaTime + 0.5f * acceleration.z * Mathf.Pow(Time.deltaTime, 2);

        gameObject.transform.position = new Vector3(positionX, 0, positionZ);

        velocity += acceleration * Time.deltaTime;


        thrust = new Vector3(angularForce * Mathf.Sin(90 * Mathf.Deg2Rad) *10000, 0, angularForce * Mathf.Cos(90 * Mathf.Deg2Rad) * 10000);

        rLeft = new Vector3(2 - comPosition.x, 0, -4 - comPosition.z);
        rRight = new Vector3(-2 - comPosition.x, 0, -4 - comPosition.z);

        torqueLeft.y = rLeft.z * thrust.x - rLeft.x * thrust.z;
        torqueRight.y = rRight.z * thrust.x - rRight.x * thrust.z;

        accLeft = torqueLeft / momentOfInertia;
        accRight = torqueRight / momentOfInertia;

        gameObject.transform.eulerAngles += new Vector3(0, rotationaVelocity *Time.deltaTime + 0.5f * accLeft.y * Mathf.Pow(Time.deltaTime, 2) * Mathf.Rad2Deg, 0);

        rotationaVelocity += accLeft.y;
    }
}

