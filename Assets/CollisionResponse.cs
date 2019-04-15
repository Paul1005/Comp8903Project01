using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResponse : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("CollisionManager").GetComponent<CollisionManager>().hasCollided = true;
        GameObject.Find("TorqueManager").GetComponent<CollisionManager>().hasCollided = true;

        if (other.gameObject.name == "Target")
        {
            float x = Mathf.Abs(gameObject.transform.position.x - other.gameObject.transform.position.x);
            float z = Mathf.Sqrt(1 - Mathf.Pow(x, 2));
            Debug.Log(z);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, other.gameObject.transform.position.z - z);
        }
    }
}
