using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public float initialVelocity;
    private float vy;
    private float vz;
    private float gravity;
    public float range;
    public float angle;
    private GameObject gunball;
    public float time;

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
        gunball.GetComponent<Rigidbody>().velocity = new Vector3(0, vy, vz);
    }

    void FixedUpdate()
    {
        if (gunball.transform.position.y < 0.05 && gunball.GetComponent<Rigidbody>().velocity.y <= 0)
        {
            vy = 0;
            vz = 0;
        }
        else
        {
            vy = vy + gravity * Time.deltaTime;
            time = time + Time.deltaTime;
        }
        gunball.GetComponent<Rigidbody>().velocity = new Vector3(0, vy, vz);
    }
}
