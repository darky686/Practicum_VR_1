using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float thrust = 1.0f;
    private float dist = 0.0f;
    private float dt1 = 0.0f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float koef = thrust / rb.mass;
        float dt = Time.time+0.0005f;


        //rb.AddForce(transform.forward * thrust, ForceMode.Force);
        if (Time.time <= 10)
        {
            rb.AddForce(transform.forward * thrust, ForceMode.Force);
            dist = koef * dt * dt / 2;
            
        }
        if ((Time.time > 10) && (Time.time <= 20))
        {
            rb.AddForce(-transform.forward * thrust, ForceMode.Force);
            dist = -koef * (20 - dt) * (20 - dt) / 2 + koef * 10 * 10;
        }
        if (Time.time > 20)
        {
            //Debug.Log("Пролетел");
           // Debug.Log(transform.position.z);
        }
        Debug.Log("Distance \n");
        Debug.Log(dist);
        Debug.Log("Z\n");
        Debug.Log(transform.position.z);
        Debug.Log("Соотношение\n");
        Debug.Log(dist / transform.position.z);
    }
}
