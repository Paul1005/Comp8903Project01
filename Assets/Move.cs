using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    GameObject boat;

    public float mass = 0;
    public float centerOfMassZ = 0;
    public float centerOfMassX = 0;
    public float momentOfInertia = 0;

    //initial input params
    public float initialVelocity;
    public float initialAcceleration;
    public float totalTime;

    public double finalVelocity;
    public double stopPosition;
    public double distance;
    public double dragConstant;
    public float deltaTime;
    public float currentTime;
    public float startTime;
    public float actualBoatPosition;
    public int frames;
    public float time;

    public int thing;

    private float initialPosition;
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        deltaTime = startTime;
        boat = GameObject.Find("Boat");
        initialPosition = boat.transform.position.z;
        boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, initialVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        mass = 0;
        centerOfMassZ = 0;
        centerOfMassX = 0;
        momentOfInertia = 0;

        //Vector3 force = new Vector3(Input.GetAxis("Horizontal") * 1000000, 0, Input.GetAxis("Vertical") * 1000000);
        //boat.GetComponent<Rigidbody>().AddForce(force);

        Rigidbody[] bodies = boat.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            string name = body.gameObject.name;
            if (name != "Boat")
            {
                //Debug.Log("Mass of " + name + ": " + body.mass);
                // Debug.Log("Position of " + name + ": " + body.gameObject.transform.localPosition);
                mass += body.mass;
            }
            centerOfMassX += body.gameObject.transform.localPosition.x * body.mass;
            centerOfMassZ += body.gameObject.transform.localPosition.z * body.mass;
        }

        //Debug.Log("Mass of " + boat.name + ": " + mass);
        boat.GetComponent<Rigidbody>().mass = mass;
        centerOfMassZ /= mass;
        centerOfMassX /= mass;
        //Debug.Log("Center of mass for " + boat.name + ": " + centerOfMassZ + ", " + centerOfMassX);

        foreach (Rigidbody body in bodies)
        {
            string name = body.gameObject.name;
            if (name != "Boat")
            {
                float h2 = Mathf.Pow(centerOfMassX - body.gameObject.transform.localPosition.x, 2) + Mathf.Pow(centerOfMassZ - body.gameObject.transform.localPosition.z, 2);
                //Debug.Log("h^2 for " + name + ": " + h2);
                float Mh2 = h2 * body.mass;
                //Debug.Log("Mh^2 for " + name + ": " + Mh2);
                float ly = body.mass * (Mathf.Pow(body.gameObject.transform.localScale.x, 2) + Mathf.Pow(body.gameObject.transform.localScale.z, 2)) / 12;
                float inertia = ly + Mh2;
                //Debug.Log("Moment of Inertia for " + name + ": " + inertia);

                momentOfInertia += inertia;
            }
        }

        //Debug.Log("moment of Inertia for " + boat.name + ": " + momentOfInertia);
    }

    private void FixedUpdate()
    {
        time = Time.deltaTime;
        deltaTime += time;

        if (thing == 0)
        {
            if (boat.GetComponent<Rigidbody>().velocity.z > 0)
            {
                frames++;
                currentTime = deltaTime - startTime;
                //Debug.Log(boat.GetComponent<Rigidbody>().velocity);
                //Debug.Log(currentTime + " " + totalTime);

                distance = initialPosition + initialVelocity * currentTime + 0.5 * initialAcceleration * Mathf.Pow(currentTime, 2);

                boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, initialVelocity + initialAcceleration * currentTime);
            }
            if (currentTime >= totalTime)
            {
                boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                stopPosition = distance;
            }
            if (boat.GetComponent<Rigidbody>().velocity.z < 0)
            {
                boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
        else if (thing == 1)
        {
            if (boat.GetComponent<Rigidbody>().velocity.z <= 0 || boat.transform.position.z >=stopPosition)
            {
                boat.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                totalTime = currentTime;
                distance = boat.transform.position.z;
                //Debug.Log(totalTime);
                //Debug.Log(boat.transform.position.z);
            }
            else
            {
                //Debug.Log(distance + " " + stopPosition);
                dragConstant = -initialAcceleration / Mathf.Pow(initialVelocity, 2);
                //Debug.Log(dragConstant);
                //Debug.Log(boat.GetComponent<Rigidbody>().velocity.z);
                //Debug.Log(Mathf.Pow(boat.GetComponent<Rigidbody>().velocity.z, 2));
                //Debug.Log(dragConstant);
                distance = initialPosition + (Mathf.Log(1 + initialVelocity * currentTime, (float)Math.E) / dragConstant);
                //Debug.Log(initialVelocity / (1 + dragConstant * initialVelocity * currentTime));
                boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, (float)(initialVelocity / (1 + dragConstant * initialVelocity * currentTime)));
                currentTime = deltaTime - startTime;
                frames++;
            }
        }
        actualBoatPosition = boat.transform.position.z;
    }
}
