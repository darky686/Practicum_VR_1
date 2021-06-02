using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilot : MonoBehaviour
{
	public float mu;
	public float d_mu;
	public float F;
	public float M;
	public float manual_F;
	public float tkrot;
	public float tkflight;
	public GameObject Target;
	public GameObject Camera;
	
	private int switcher = 1;
	public float distance=0f;
	public float angle=0f;
	public Vector3 tensor;
    public Rigidbody rb;
	public float MM=0.32f;
	
	private float start_time=0f;
	private bool flag=false;
	private float t_min=0f;
	private float t_max=0f;
	private float sqrt=0f;
	
    // Start is called before the first frame update
    void Start()
    {	Time.timeScale=1;
        rb = GetComponent<Rigidbody>();
		rb.inertiaTensor = tensor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Debug.Log(switcher);
		switch (switcher)
		{
			case 1:
			if (Input.GetKeyDown("m"))
			{	Debug.Log("Trying Raycast");
				distance=Raycast().y;
				angle=Mathf.Deg2Rad*Raycast().z;
				if (distance!=0)
					{start_time=Time.time;
					switcher=2;
					Debug.Log("Autorot");}
				else 
					Debug.Log("Try again");
			}
			break;
			
			case 2:
			AutoRot(angle,start_time);
			break;

			case 3:			
			Mover(distance);
			break;
		}
    }
	
	Vector3 Raycast()
	{
		RaycastHit hit;
		float q_y=0;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out hit))
		{if (hit.collider.gameObject == Target)
			{
				Debug.Log("Found an object - distance: " + hit.distance);
				Quaternion q_rot = Camera.transform.localRotation; 
				Debug.Log(q_rot.eulerAngles);
				q_y=q_rot.eulerAngles.y;
				return new Vector3(0, hit.distance, q_y);
				
			}
		else return new Vector3(0, 0,0);}
		else return new Vector3(0, 0,0);
	}
	
	void AutoRot(float ang, float start_time)
	{	int koef=1;
		if (ang>Mathf.PI)
				ang=-(2*Mathf.PI-ang);
		if (ang<-Mathf.PI)
				ang=(2*Mathf.PI+ang);
		if (ang<0)
			koef=-1;
		else
			koef=1;
		if (flag==false){
			t_min=tkrot/2-Mathf.Sqrt(tkrot*tkrot/4-koef*ang*tensor.x/MM);
				Debug.Log(t_min);
			t_max=tkrot/2+Mathf.Sqrt(tkrot*tkrot/4-koef*ang*tensor.x/MM);
				Debug.Log(t_max);
			flag=true;}
			
		if ((ang>0)&&(Time.time-start_time<t_min))
			{//Debug.Log("One side");
			rb.AddTorque(transform.up * MM, ForceMode.Force);}
		if ((ang>0)&&(Time.time-start_time>t_max)&&(Time.time-start_time<tkrot))
			{//Debug.Log("Second side");
			rb.AddTorque(-transform.up * MM, ForceMode.Force);}
		
		if ((ang<0)&&(Time.time-start_time<t_min))
			{//Debug.Log("One side ang -");
			rb.AddTorque(-transform.up * MM, ForceMode.Force);}
		if ((ang<0)&&(Time.time-start_time>t_max)&&(Time.time-start_time<tkrot))
			{//Debug.Log("Second side ang -");
			rb.AddTorque(transform.up * MM, ForceMode.Force);}
		
	
	
		if(Time.time-start_time>tkrot)
			{switcher=3;
			flag=false;
			Debug.Log("Switched 3");}
		
	}
	
	void Mover(float distance)
	{//rb.AddForce(transform.right * 4.0f, ForceMode.Force); 
		if (flag==false){
			t_min=tkflight/2-Mathf.Sqrt(tkflight*tkflight/4-distance*rb.mass/F);
				Debug.Log(t_min);
			t_max=tkflight/2+Mathf.Sqrt(tkflight*tkflight/4-distance*rb.mass/F);
				Debug.Log(t_max);
			start_time=Time.time;
			flag=true;

			}
		
		if (Time.time-start_time<t_min)
			{		
			rb.AddForce(transform.forward * F, ForceMode.Force);
 
			rb.AddTorque(transform.up * 0.003f, ForceMode.Force);
			rb.AddForce(transform.forward * -0.02f, ForceMode.Force);
			
			}
		
		if ((Time.time-start_time>t_max)&&(Time.time-start_time<tkflight))
		{
		rb.AddForce(-transform.forward * F, ForceMode.Force);
 
		//rb.AddTorque(transform.up * -0.04f, ForceMode.Force);
		rb.AddForce(transform.forward *- 0.5f, ForceMode.Force);
		
		}
		 
		if(Time.time-start_time>tkflight)
			{switcher=1;
			flag=false;
			Debug.Log(rb.position);
			sqrt=(rb.position.x*rb.position.x + (rb.position.z+0.47012f)*(rb.position.z+0.47012f));
			Debug.Log(100*( 0.0311189f)/Mathf.Sqrt(sqrt));
			Time.timeScale=0; 
			Debug.Log("Switched 1");}
		
	}
	
}
