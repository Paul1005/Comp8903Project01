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
            Debug.Log(body.mass);
            Debug.Log(body.position);

            mass += body.mass;
            centerOfMass += body.position.z * body.mass;

        }
        Debug.Log(mass);
        boat.GetComponent<Rigidbody>().mass = mass;
        centerOfMass /= mass;
        Debug.Log(centerOfMass);

        foreach (Rigidbody body in bodies)
        {
            Debug.Log(Mathf.Pow(centerOfMass - body.position.z, 2));
            Debug.Log(Mathf.Pow(centerOfMass - body.position.z, 2) * body.mass);
            Debug.Log(Mathf.Pow(centerOfMass - body.position.z, 2) * body.mass + body.mass * (Mathf.Pow(body.gameObject.transform.localScale.x, 2) + Mathf.Pow(body.gameObject.transform.localScale.z, 2)) / 12);
            momentOfInertia += Mathf.Pow(centerOfMass - body.position.z, 2) * body.mass + body.mass * (Mathf.Pow(body.gameObject.transform.localScale.x, 2) + Mathf.Pow(body.gameObject.transform.localScale.z, 2)) / 12;
        }

        Debug.Log(momentOfInertia);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = new Vector3(Input.GetAxis("Horizontal") * 1000000, 0, Input.GetAxis("Vertical") * 1000000);
        boat.GetComponent<Rigidbody>().AddForce(force);


    }
}
