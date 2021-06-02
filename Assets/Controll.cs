using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controll : MonoBehaviour
{
    public float thrust = 1.0f;
    public float torque;
    public float DeltaTime;
    public float force = 1000.0f;
    public Rigidbody rb;
    private bool mode = true;
    private bool space = false;
    private float timer = 0.0f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
       
        if (Input.GetKeyDown("e"))
        { mode = !mode;
            Debug.Log("Switch mode");

                if (mode)
                rb.angularDrag = 10.0f;
                else
                rb.angularDrag = 0.0f;
        }
            


        if (Input.GetKey("right"))
        {
            if (mode)
            {
                rb.AddForce(transform.right * thrust, ForceMode.Force);
            }
            else
            {
                rb.AddTorque(transform.up * torque, ForceMode.Force);
                
                
            }
            //Debug.Log("Test");
                
        }

        if (Input.GetKey("left"))
        {
            if (mode)
            {
                rb.AddForce(-transform.right * thrust, ForceMode.Force);
            }
            else
            {
                rb.AddTorque(-transform.up * torque, ForceMode.Force);
                
                
            }
        }
        if (Input.GetKey("up"))
        {
            if (mode)
            {
                rb.AddForce(transform.forward * thrust, ForceMode.Force);
                
            }


            else
            {
                rb.AddTorque(-transform.right * torque, ForceMode.Force);
            }
        }
        if (Input.GetKey("down"))
        {
            if (mode)
            {
                rb.AddForce(-transform.forward * thrust, ForceMode.Force);
            }


            else
            {
                rb.AddTorque(transform.right * torque, ForceMode.Force);
            }

        }
        if (Input.GetKey("w"))
        {
            if (mode)
            {
                rb.AddForce(transform.up * thrust, ForceMode.Force);
            }
            else
            {
                rb.AddTorque(-transform.forward * torque, ForceMode.Force);
            }


        }
        if (Input.GetKey("s"))
        {
            if (mode)
            {
                rb.AddForce(-transform.up * thrust, ForceMode.Force);
            }
            else
            {
                rb.AddTorque(transform.forward * torque, ForceMode.Force);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            timer = Time.time;
            space = true;
        }

        if (space == true)
        { 


            if (Time.time<timer+DeltaTime)
            {
                Debug.Log("first");
                rb.AddForce(transform.forward * force, ForceMode.Force);
            }
            if ((Time.time < timer + 2*DeltaTime)&& (Time.time >= timer +  DeltaTime))
            {
                Debug.Log("second");
                //rb.AddForce(-transform.forward * force, ForceMode.Force);
            }
            if (Time.time > timer + 2 * DeltaTime)
                space = false;

        }


    }
}