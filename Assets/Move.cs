using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    GameObject boat;
    float mass;
    float centerOfMass;
    float momentOfInertia;

    // Use this for initialization
    void Start()
    {
        boat = GameObject.Find("Boat");
        Rigidbody[] bodies = boat.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            string name = body.gameObject.name;
            if (name != "Boat")
            {
                Debug.Log("Mass of " + name + ": " + body.mass);
                Debug.Log("Position of " + name + ": " + body.position);
            }

            mass += body.mass;
            centerOfMass += body.position.z * body.mass;

        }
        Debug.Log("Mass of " + boat.name + ": " + mass);
        boat.GetComponent<Rigidbody>().mass = mass;
        centerOfMass /= mass;
        Debug.Log("Center of mass for " + boat.name + ": " + centerOfMass);

        foreach (Rigidbody body in bodies)
        {
            string name = body.gameObject.name;
            if (name != "Boat")
            {
                float h2 = Mathf.Pow(centerOfMass - body.position.z, 2);
                Debug.Log("h^2 for " + name + ": " + h2);
                float Mh2 = h2 * body.mass;
                Debug.Log("Mh^2 for " + name + ": " + Mh2);
                float ly = body.mass * (Mathf.Pow(body.gameObject.transform.localScale.x, 2) + Mathf.Pow(body.gameObject.transform.localScale.z, 2)) / 12;
                float inertia = ly + Mh2;
                Debug.Log("Moment of Inertia for " + name + ": " + inertia);

                momentOfInertia += inertia;
            }
        }

        Debug.Log("moment of Inertia for " + boat.name + ": " + momentOfInertia);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = new Vector3(Input.GetAxis("Horizontal") * 1000000, 0, Input.GetAxis("Vertical") * 1000000);
        boat.GetComponent<Rigidbody>().AddForce(force);


    }
}
