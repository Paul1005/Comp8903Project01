using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float initialVelocity;
    public float vy;
    public float vz;
    public float vx;
    private float ivy;
    private float ivz;
    private float ivx;
    private float py;
    private float pz;
    private float px;
    private float ipy;
    private float ipz;
    private float ipx;
    private float gravity;
    public float rangeZ;
    public float rangeX;
    public float angle;
    public float alphaAngle;
    public float gammaAngle;
    private GameObject gunball;
    public float time;
    private bool wasFired;
    public int ticks;
    private Vector3 windVelocity;
    public int angularOmega_i;
    public float angularOmega_f;

    public int angularAlpha;
    public float angularTheta;
    public float mass;
    public float dragCoefficient;
    public float tau;
    public float windSpeed;
    public float windCoefficient;

    // Use this for initialization
    void Start()
    {
        GameObject target = GameObject.Find("Target");
        gravity = -9.81f;
        rangeZ = target.transform.position.z - gameObject.transform.position.z;
        rangeX = target.transform.position.x - gameObject.transform.position.x;
        gunball = GameObject.Find("Gunball");

        //do calculations here
        angle = Mathf.Rad2Deg * Mathf.Asin(gravity * Mathf.Sqrt(Mathf.Pow(rangeZ, 2) + Mathf.Pow(rangeX, 2)) / Mathf.Pow(initialVelocity, 2)) / 2; //=ASIN(9.81*SQRT(B10^2+B12^2)/B13^2)
        alphaAngle = 90 + angle;
        gammaAngle = Mathf.Asin(rangeX / Mathf.Sqrt(Mathf.Pow(rangeZ, 2) + Mathf.Pow(rangeX, 2))) * Mathf.Rad2Deg;//=ASIN(B12/SQRT(B10^2+B12^2))
        gameObject.transform.eulerAngles = new Vector3(angle, gammaAngle, 0);

        vy = initialVelocity * Mathf.Cos(alphaAngle * Mathf.Deg2Rad);
        vx = initialVelocity * Mathf.Sin(alphaAngle * Mathf.Deg2Rad) * Mathf.Sin(gammaAngle * Mathf.Deg2Rad);
        vz = initialVelocity * Mathf.Sin(alphaAngle * Mathf.Deg2Rad) * Mathf.Cos(gammaAngle * Mathf.Deg2Rad);
        ivy = vy;
        ivx = vx;
        ivz = vz;
        ipz = gunball.transform.position.z;
        ipx = gunball.transform.position.x;
        ipy = gunball.transform.position.z;
        tau = mass / dragCoefficient;
        windVelocity = new Vector3(windCoefficient * windSpeed * Mathf.Cos(gammaAngle) / dragCoefficient, 0, windCoefficient * windSpeed * Mathf.Sin(gammaAngle) / dragCoefficient);

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
            ticks++;

            py = ipy + ivy * tau * (1 - Mathf.Exp(-time / tau)) + gravity * Mathf.Pow(tau, 2) * (1 - Mathf.Exp(-time / tau)) - gravity * tau * time;

            px = ipx + ivx * tau * (1 - Mathf.Exp(-time / tau)) + windVelocity.x * tau * (1 - Mathf.Exp(-time / tau)) - windVelocity.x * time;

            pz = ipz + ivz * tau * (1 - Mathf.Exp(-time / tau)) + windVelocity.z * tau * (1 - Mathf.Exp(-time / tau)) - windVelocity.z * time;

            gunball.transform.position = new Vector3(px, py, pz);
            vx = Mathf.Exp(-time / tau) * vx + (Mathf.Exp(-time / tau) - 1) * windVelocity.x;
            vy = Mathf.Exp(-time / tau) * vx + (Mathf.Exp(-time / tau) - 1) * gravity * tau;
            vz = Mathf.Exp(-time / tau) * vz + (Mathf.Exp(-time / tau) - 1) * windVelocity.z;
            angularOmega_f = angularOmega_i + time * angularAlpha;
            angularTheta = angularOmega_i * time + angularAlpha * time * time / 2;
            gunball.transform.eulerAngles = new Vector3(angularTheta * Mathf.Rad2Deg, 0, 0);
        }

        wasFired = true;
    }
}
