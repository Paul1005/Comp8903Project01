﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedMove : MonoBehaviour
{
    public Vector3 dinZ;
    public float force;
    public float angle;
    public int hullMass;
    public int gunMass;
    public int pilotMass;
    public float time;
    public Vector3 vInitial;
    public Vector3 vFinal;

    private int comMass;
    private Vector3 acceleration;
    private Vector3 comPosition;
    private GameObject hull;
    private GameObject gun;
    private GameObject pilot;
    public int ticks;
    // Use this for initialization
    void Start()
    {
        ticks = 0;
        comMass = hullMass + gunMass + pilotMass;
        Vector3 thrust = new Vector3(force * Mathf.Sin(angle * Mathf.Deg2Rad), 0, force * Mathf.Cos(angle * Mathf.Deg2Rad));
        acceleration = new Vector3(thrust.x / comMass, thrust.y / comMass, thrust.z / comMass);
        hull = GameObject.Find("Hull");
        gun = GameObject.Find("Gun");
        pilot = GameObject.Find("Pilot");
        gameObject.transform.eulerAngles = new Vector3(0, angle, 0);
        comPosition.x = (hull.transform.position.x * hullMass + gun.transform.position.x * gunMass + pilot.transform.position.x * pilotMass) / comMass;
        comPosition.z = (hull.transform.position.z * hullMass + gun.transform.position.z * gunMass + pilot.transform.position.z * pilotMass) / comMass;
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.z <= dinZ.z)
        {
            ticks++;
            time += Time.deltaTime;
            float positionX = vInitial.x * time + 0.5f * acceleration.x * Mathf.Pow(time, 2);
            float positionZ = vInitial.z * time + 0.5f * acceleration.z * Mathf.Pow(time, 2);
            gameObject.transform.position = new Vector3(positionX, 0, positionZ);
            vFinal = new Vector3(vInitial.x + acceleration.x * time, vInitial.y + acceleration.y * time, vInitial.z + acceleration.z * time);
        }
    }
}