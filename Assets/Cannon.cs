using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public float initialVelocity;
    private float gravity;
    public float range;
    public float angle;

    // Use this for initialization
    void Start()
    {
        GameObject target = GameObject.Find("Target");
        gravity = 9.81f;
        range = target.transform.position.z - gameObject.transform.position.z;
        GameObject gunball = GameObject.Find("Gunball");

        //do calculations here
        angle = Mathf.Rad2Deg * (Mathf.Asin(gravity * range / (Mathf.Pow(initialVelocity, 2) * 2)));
        gameObject.transform.eulerAngles = new Vector3(-angle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
