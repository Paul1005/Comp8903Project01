using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularMove : MonoBehaviour
{
    public float force;
    public Vector3 dFinalLeft;
    public Vector3 dFinalRight;
    public float momentOfInertia;
    private bool left;
    private bool right;

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
    public GameObject hull;
    public GameObject gun;
    public GameObject pilot;
    public int comMass;
    public int ticks;

    void Start()
    {
        right = false;
        left = false;
        ticks = 0;
        EnhancedMove enhancedMove = gameObject.GetComponent<EnhancedMove>();
        comMass = 0;// enhancedMove.hullMass + enhancedMove.gunMass + enhancedMove.pilotMass;

        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");

        comPosition.x = (hull.transform.position.x * enhancedMove.hullMass + gun.transform.position.x * enhancedMove.gunMass + pilot.transform.position.x * enhancedMove.pilotMass) / comMass;
        comPosition.z = (hull.transform.position.z * enhancedMove.hullMass + gun.transform.position.z * enhancedMove.gunMass + pilot.transform.position.z * enhancedMove.pilotMass) / comMass;

        float lyHull = enhancedMove.hullMass * (Mathf.Pow(hull.transform.localScale.x, 2) + Mathf.Pow(hull.transform.localScale.z, 2)) / 12;
        float h2Hull = Mathf.Pow(comPosition.x - hull.transform.position.x, 2) + Mathf.Pow(comPosition.z - hull.transform.position.z, 2);
        float mh2Hull = h2Hull * enhancedMove.hullMass;
        float inertiaHull = lyHull + mh2Hull;

        float lyPilot = enhancedMove.hullMass * (Mathf.Pow(pilot.transform.localScale.x, 2) + Mathf.Pow(pilot.transform.localScale.z, 2)) / 12;
        float h2Pilot = Mathf.Pow(comPosition.x - pilot.transform.position.x, 2) + Mathf.Pow(comPosition.z - pilot.transform.position.z, 2);
        float mh2Pilot = h2Pilot * enhancedMove.pilotMass;
        float inertiaPilot = lyPilot + mh2Pilot;

        momentOfInertia = inertiaHull + inertiaPilot;

        thrust = new Vector3(/*force * Mathf.Sin(enhancedMove.angle * Mathf.Deg2Rad), 0, force * Mathf.Cos(enhancedMove.angle * Mathf.Deg2Rad)*/);

        rLeft = new Vector3(2 - comPosition.x, 0, -4 - comPosition.z);
        rRight = new Vector3(-2 - comPosition.x, 0, -4 - comPosition.z);

        torqueLeft.y = rLeft.z * thrust.x - rLeft.x * thrust.z;
        torqueRight.y = rRight.z * thrust.x - rRight.x * thrust.z;

        accLeft = torqueLeft / momentOfInertia;
        accRight = torqueRight / momentOfInertia;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = true;
            right = false;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            right = true;
            left = false;
        }

        if (left)
        {
            if (Mathf.Abs(0.5f * accLeft.y * Mathf.Pow(tFinalLeft, 2)) < dFinalLeft.y)
            {
                ticks++;
                tFinalLeft += Time.deltaTime;
                gameObject.transform.eulerAngles = new Vector3(0, 0.5f * accLeft.y * Mathf.Pow(tFinalLeft, 2) * Mathf.Rad2Deg, 0);
            }
        }
        else if (right)
        {
            if (Mathf.Abs(0.5f * accRight.y * Mathf.Pow(tFinalRight, 2)) < dFinalRight.y)
            {
                ticks++;
                tFinalRight += Time.deltaTime;
                gameObject.transform.eulerAngles = new Vector3(0, 0.5f * accRight.y * Mathf.Pow(tFinalRight, 2) * Mathf.Rad2Deg, 0);
            }
        }
    }
}
