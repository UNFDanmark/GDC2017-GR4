using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public string horizontalAxis;
    public string verticalAxis;
    public string chargeDashAxis;
    [Space(20)]
    public float chargeMax = 5;
    public float pushForce = 10;
    public float walkSpeed = 1;
    public float dashForce = 25;
    [Space(20)]
    public Vector3 spawn; 
    [Space(20)]
    public bool charging = false;
    public bool onGround = false;
    public float chargeStart = 0;
    public float chargeDir = 0;
    [Space(20)]
    public Rigidbody body;
    public Collider collision;




    void Awake ()
    {
        body = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Charge();
        //print(Input.GetAxis(chargeDashAxis)+ " " + Input.GetAxis(verticalAxis) + " " + Input.GetAxis(horizontalAxis));
    }

    void FixedUpdate()
    {
        Move(walkSpeed);
        if (transform.position.y < -10)
        {
            KO();
        }
        
    }

    

    public void Move(float speed)
    {
        if (onGround)
        {

            body.velocity = new Vector3(body.velocity.x + speed * Input.GetAxis(horizontalAxis), body.velocity.y, body.velocity.z);
        }
    }

    public void Charge()
    {
        if(Input.GetAxis(chargeDashAxis) == 1)
        {
            if (!charging)
            {
                chargeStart = Time.time;
                charging = true;
            }
        }
        else
        {
            if(charging)
            {
                Dash();
                charging = false;
            }
        }
    }

    public void Dash()
    {
        float chargeTime = Mathf.Min(chargeMax, Time.time - chargeStart);
        body.velocity = body.velocity + new Vector3(0, dashForce * chargeTime, 0);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == gameObject.tag)
        {
            Rigidbody other = col.gameObject.GetComponent<Rigidbody>();
            if (other.velocity.magnitude > body.velocity.magnitude)
            {
                body.velocity = body.velocity + (body.position - other.position).normalized * pushForce * other.velocity.magnitude;
            }
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag != gameObject.tag)
        {
            onGround = true;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag != gameObject.tag)
        {
            onGround = false;
        }
    }

    public void KO()
    {
        transform.position = spawn;
        body.velocity = new Vector3(0, 0, 0);
        body.angularVelocity = new Vector3(0, 0, 0);
    }
}
