using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public string horizontalAxis;
    public string verticalAxis;
    public string chargeDashAxis;
    public float walkSpeed = 1;
    public Rigidbody body;
    public float chargeStart = 0;
    public bool charging = false;
    public float dashForce = 25;

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
        //print(Input.GetAxis(chargeDashAxis)+ " " + Input.GetAxis(verticalAxis) + " " + Input.GetAxis(horizontalAxis));
	}

    void FixedUpdate()
    {
        Move(walkSpeed);
        Charge();
    }


    public void Move(float speed)
    {
        body.velocity = new Vector3(body.velocity.x + speed * Input.GetAxis(horizontalAxis), body.velocity.y, body.velocity.z);
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
        float chargeTime = Time.time - chargeStart;
        body.velocity = body.velocity + new Vector3(0, dashForce * chargeTime, 0);
    }
}
