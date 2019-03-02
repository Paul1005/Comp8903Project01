using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularMove : MonoBehaviour
{
    public float force;
    public Vector3 dFinalLeft;
    public Vector3 dFinalRight;
    public int hullMass;
    public int gunMass;
    public int pilotMass;
    public float momentOfInertia;

    public Vector3 tFinalLeft;
    public Vector3 tFinalRight;
    private Vector3 rLeft;
    private Vector3 rRight;
    private Vector3 torqueLeft;
    private Vector3 torqueRight;
    public Vector3 accLeft;
    public Vector3 accRight;
    private Vector3 thrust;
    private Vector3 comPosition;
    private GameObject hull;
    private GameObject gun;
    private GameObject pilot;
    private int comMass;
    private int ticks;
    public float angle;

    // Use this for initialization
    void Start()
    {
        ticks = 0;

        comMass = hullMass + gunMass + pilotMass;
        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");

        comPosition.x = (hull.transform.position.x * hullMass + gun.transform.position.x * gunMass + pilot.transform.position.x * pilotMass) / comMass;
        comPosition.z = (hull.transform.position.z * hullMass + gun.transform.position.z * gunMass + pilot.transform.position.z * pilotMass) / comMass;

        float lyHull = hullMass * (Mathf.Pow(hull.transform.localScale.x, 2) * Mathf.Pow(hull.transform.localScale.z, 2)) / 12;
        float h2Hull = Mathf.Pow(comPosition.x-hull.transform.position.x, 2) + Mathf.Pow(comPosition.z - hull.transform.position.z, 2);
        float mh2Hull = h2Hull * hullMass;
        float inertiaHull = lyHull + mh2Hull;

        float lyPilot = hullMass * (Mathf.Pow(pilot.transform.localScale.x, 2) * Mathf.Pow(pilot.transform.localScale.z, 2)) / 12;
        float h2Pilot = Mathf.Pow(comPosition.x - pilot.transform.position.x, 2) + Mathf.Pow(comPosition.z - pilot.transform.position.z, 2);
        float mh2Pilot = h2Pilot * pilotMass;
        float inertiaPilot = lyPilot + mh2Pilot;

        momentOfInertia = inertiaHull + inertiaPilot;

        thrust = new Vector3(force * Mathf.Sin(angle * Mathf.Deg2Rad), 0, force * Mathf.Cos(angle * Mathf.Deg2Rad));

        rLeft = new Vector3(2 - comPosition.x, 0, 4 - comPosition.z);
        rRight = new Vector3(-2 - comPosition.x, 0, -4 - comPosition.z);

        torqueLeft.y = rLeft.z * thrust.x - rLeft.x * thrust.z;
        torqueRight.y = rRight.z * thrust.x - rRight.x * thrust.z;

        gameObject.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
