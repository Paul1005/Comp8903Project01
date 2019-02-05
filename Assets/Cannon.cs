using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public float initialVelocity;
    private float vy;
    private float vz;
    private float py;
    private float pz;
    private float ipz;
    private float gravity;
    public float range;
    public float angle;
    private GameObject gunball;
    public float time;
    private bool wasFired;

    // Use this for initialization
    void Start()
    {
        GameObject target = GameObject.Find("Target");
        gravity = -9.81f;
        range = target.transform.position.z - gameObject.transform.position.z;
        gunball = GameObject.Find("Gunball");

        //do calculations here
        angle = Mathf.Rad2Deg * Mathf.Asin(gravity * range / Mathf.Pow(initialVelocity, 2)) / 2;
        gameObject.transform.eulerAngles = new Vector3(angle, 0, 0);

        vy = -initialVelocity * Mathf.Sin(angle * Mathf.Deg2Rad);
        vz = initialVelocity * Mathf.Cos(angle * Mathf.Deg2Rad);
        ipz = gunball.transform.position.z;
        //gunball.GetComponent<Rigidbody>().velocity = new Vector3(0, vy, vz);
        wasFired = false;
    }

    void FixedUpdate()
    {
        if (wasFired && gunball.transform.position.y < 0.05)
        {

        }
        else
        {
            time = time + Time.deltaTime;

            py = -initialVelocity * Mathf.Sin(angle * Mathf.Deg2Rad) * time + 0.5f * gravity * Mathf.Pow(time, 2);
            pz = ipz + vz * time;
            gunball.transform.position = new Vector3(gunball.transform.position.x, py, pz);
        }

        wasFired = true;
    }
}
