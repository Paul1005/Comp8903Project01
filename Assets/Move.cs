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

    // Use this for initialization
    void Start()
    {
        boat = GameObject.Find("Boat");
    }

    // Update is called once per frame
    void Update()
    {
         mass = 0;
         centerOfMassZ = 0;
         centerOfMassX = 0;
         momentOfInertia = 0;
        Vector3 force = new Vector3(Input.GetAxis("Horizontal") * 1000000, 0, Input.GetAxis("Vertical") * 1000000);
        boat.GetComponent<Rigidbody>().AddForce(force);

        Rigidbody[] bodies = boat.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            string name = body.gameObject.name;
            if (name != "Boat")
            {
                Debug.Log("Mass of " + name + ": " + body.mass);
                Debug.Log("Position of " + name + ": " + body.gameObject.transform.localPosition);
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
                Debug.Log("h^2 for " + name + ": " + h2);
                float Mh2 = h2 * body.mass;
                Debug.Log("Mh^2 for " + name + ": " + Mh2);
                float ly = body.mass * (Mathf.Pow(body.gameObject.transform.localScale.x, 2) + Mathf.Pow(body.gameObject.transform.localScale.z, 2)) / 12;
                float inertia = ly + Mh2;
                Debug.Log("Moment of Inertia for " + name + ": " + inertia);

                momentOfInertia += inertia;
            }
        }

        //Debug.Log("moment of Inertia for " + boat.name + ": " + momentOfInertia);
    }
}
