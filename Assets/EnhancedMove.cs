using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedMove : MonoBehaviour
{
    public float dinZ;
    public float force;
    public float angle;
    public int hullMass;
    public int gunMass;
    public int pilotMass;
    public float Time { get; }
    public float vInitial;
    public float VFinal { get; }

    private int comMass;
    private float acceleration;
    public Vector3 comPosition;
    private GameObject hull;
    private GameObject gun;
    private GameObject pilot;

    // Use this for initialization
    void Start()
    {
        comMass = hullMass + gunMass + pilotMass;
        acceleration = force / comMass;
        //= ($B$2 * E2 +$B$4 * E4 +$B$3 * E3)/$B$5
        //= ($B$2 * F2 +$B$4 * F4 +$B$3 * F3)/$B$5
        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");

        comPosition.x = (hull.transform.position.x * hullMass + gun.transform.position.x * gunMass + pilot.transform.position.x * pilotMass)/comMass;
        comPosition.z = (hull.transform.position.z * hullMass + gun.transform.position.z * gunMass + pilot.transform.position.z * pilotMass)/comMass;
    }

    private void FixedUpdate()
    {
        //comPosition = ;
    }
}
