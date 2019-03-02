using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularMove : MonoBehaviour {
    public float force;
    public Vector3 dFinalLeft;
    public Vector3 dFinalRight;
    public int hullMass;
    public int gunMass;
    public int pilotMass;

    private Vector3 tFinalLeft;
    private Vector3 tFinalRight;
    private Vector3 rLeft;
    private Vector3 rRight;
    private Vector3 torqueLeft;
    private Vector3 torqueRight;
    private Vector3 accLeft;
    private Vector3 accRight;
    private Vector3 thrust;
    private Vector3 comPosition;
    private GameObject hull;
    private GameObject gun;
    private GameObject pilot;
    private int comMass;
    private int ticks;
    // Use this for initialization
    void Start () {
        ticks = 0;
        comMass = hullMass + gunMass + pilotMass;
        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");
        comPosition.x = (hull.transform.position.x * hullMass + gun.transform.position.x * gunMass + pilot.transform.position.x * pilotMass) / comMass;
        comPosition.z = (hull.transform.position.z * hullMass + gun.transform.position.z * gunMass + pilot.transform.position.z * pilotMass) / comMass;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
