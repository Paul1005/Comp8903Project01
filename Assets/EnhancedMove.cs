using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedMove : MonoBehaviour
{
    public float force;
    public float angle;
    public int hullMass;
    public int gunMass;
    public int pilotMass;
    public float time;
    public Vector3 vInitial;
    public Vector3 vFinal;
    public Vector3 thrust;
    public float vMax;
    public float dragCoefficient;
    public float tau;
    public float totalTime;

    public int comMass;
    public Vector3 acceleration;
    public int ticks;
    // Use this for initialization
    void Start()
    {
        ticks = 0;
        comMass = hullMass + gunMass + pilotMass;
        //acceleration = new Vector3(thrust.x / comMass, thrust.y / comMass, thrust.z / comMass);
        tau = comMass / dragCoefficient;
        vFinal = vInitial;
    }

    private void FixedUpdate()
    {
        vMax = thrust.z / dragCoefficient;
        if (Input.GetKey(KeyCode.W) && time < totalTime)
        {
            time += Time.deltaTime;

            //gameObject.transform.eulerAngles = new Vector3(0, angle, 0);
            thrust = new Vector3(force * Mathf.Sin(angle * Mathf.Deg2Rad), 0, force * Mathf.Cos(angle * Mathf.Deg2Rad));
            ticks++;

            acceleration = new Vector3(
                (thrust.x - dragCoefficient * vFinal.x) / comMass,
                (thrust.y - dragCoefficient * vFinal.y) / comMass,
                (thrust.z - dragCoefficient * vFinal.z) / comMass);

            float positionZ = gameObject.transform.position.z + vMax * Time.deltaTime + (vMax - vFinal.z) * tau * (Mathf.Pow((float)Math.E, -Time.deltaTime / tau) - 1);

            vFinal = new Vector3(0, 0, vMax - Mathf.Pow((float)Math.E, -dragCoefficient * Time.deltaTime / comMass) * (vMax - vFinal.z));

            gameObject.transform.position = new Vector3(0, 0, positionZ);
            Debug.Log("Time: " + time + " X: " + positionZ + " V: " + vFinal.z + " A: " + acceleration.z);
        }
        else if (time < totalTime)
        {
            thrust = new Vector3(0, 0, 0);
            acceleration = new Vector3(
                (thrust.x - dragCoefficient * vFinal.x) / comMass,
                (thrust.y - dragCoefficient * vFinal.y) / comMass,
                (thrust.z - dragCoefficient * vFinal.z) / comMass);

            float positionZ = gameObject.transform.position.z + vMax * Time.deltaTime + (vMax - vFinal.z) * tau * (Mathf.Pow((float)Math.E, -Time.deltaTime / tau) - 1);

            vFinal = new Vector3(0, 0, vMax - Mathf.Pow((float)Math.E, -dragCoefficient * Time.deltaTime / comMass) * (vMax - vFinal.z));

            gameObject.transform.position = new Vector3(0, 0, positionZ);
        }
    }
}
