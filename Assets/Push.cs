using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

    public float initialVelocity;
    public float initialAcceleration;
    public float totalTime;
    public float stopPosition;
    public float distance;
    public float velocity;
    public float acceleration;
    public float dragConstant;
    public float deltaTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        // sf = si + vit+1/2at^2
        // vf = vi + at
        // sf = si + In(1 + kvit)/k
        // vf = vi/(1+kvit)
    }
}
