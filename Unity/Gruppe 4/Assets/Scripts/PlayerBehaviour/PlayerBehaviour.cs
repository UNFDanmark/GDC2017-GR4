using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public string horizontalAxis;   //The axis used for moving left and right and for aiming
    public string verticalAxis; //Another axis used for aiming
    public string chargeDashAxis;   //The axis used for the inputs to charge and execute a dash
    [Space(20)]
    public float chargeMax = 5; //The maximal amount of time in which the player can charge
    public float pushForce = 10;    //The amount of force with which the player pushes other objects
    public float walkSpeed = 1; //The amount of speed with which the player "walks"
    public float dashForce = 25;    //The amount of force with which the player dashes
    [Space(20)]
    public Vector3 spawn; //The spawn location for the player
    [Space(20)]
    public bool charging = false;   //A boolean that keeps track of whether the player is charging
    public bool onGround = false;   //A boolean that keeps track of whether the player is on the ground
    public float chargeStart = 0;   //A boolean that keeps track of when the player started charging their dash
    public float chargeDir = 0; //The direction in which the currently charging dash is pointed
    [Space(20)]
    public Rigidbody body;  //The object's Rigidbody
    public Collider collision;  //The object's collider




    void Awake ()
    {
        body = GetComponent<Rigidbody>();   //Sets the Rigidbody
        spawn = transform.position; //Sets the spawn position to the start position
    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Charge();   //Run the charge command
    }

    void FixedUpdate()  //Runs 50 times per second
    {
        Move(walkSpeed);    //Run the walk command with the object's walk speed
        if (KOCheck()) KO();    //Checks if the player is KO'd, and if they are, KO them
    }
    
    public void OnCollisionEnter(Collision col)    //Runs when initiating contact with other objects
    {
        if (col.gameObject.tag == gameObject.tag)    //Checks if the other object's tag matches the current tag
        {
            Rigidbody other = col.gameObject.GetComponent<Rigidbody>(); //Stores the collision object's Rigidbody in other
            if (other.velocity.magnitude > body.velocity.magnitude) //Checks if the current object moves slower than the other object
            {
                body.velocity = body.velocity + (body.position - other.position).normalized * pushForce * other.velocity.magnitude; //Shoots self away from the other player
            }
        }
    }

    public void OnCollisionStay(Collision col) //Runs while in contact with other object
    {
        if (col.gameObject.tag != gameObject.tag)   //Checks if the tags DO NOT match
        {
            onGround = true;    //Inform's the game that the player is on the ground
        }
    }

    public void OnCollisionExit(Collision col) //Runs when leaving contact with other objects
    {
        if (col.gameObject.tag != gameObject.tag)   //Checks if the tags DO NOT match
        {
            onGround = false;   //Informs the game that the player is not on ground
        }
    }


    //=====================================================================
    // UNITY BUILD IN ABOVE, OWN FUNCTIONS BELOW
    //=====================================================================


    public void Charge()    //Charge up a dash
    {
        if (Input.GetAxis(chargeDashAxis) == 1) //Check if the charge/dash buttons are being held
        {
            if (!charging)  //Check if already charging
            {
                chargeStart = Time.time;    //Set the start time of the charge
                charging = true;    //Inform the system that the charge has started
            }
        }
        else
        {
            if (charging)   //Check if previously charging
            {
                Dash(); //Launch dash
                charging = false;   //Inform the system that the charge has ended
            }
        }
    }

    public void Dash()
    {
        float chargeTime = Mathf.Min(chargeMax, Time.time - chargeStart);
        body.velocity = body.velocity + new Vector3(0, dashForce * chargeTime, 0);
    }

    public void KO()    //KO's a player and respawns them
    {
        transform.position = spawn; //Sets the player's current position to the spawn position
        body.velocity = new Vector3(0, 0, 0);   //Sets the current speed to be 0
        body.angularVelocity = new Vector3(0, 0, 0);    //Stops the current rotation
    }

    public bool KOCheck()   //Checks if the player is KO'd
    {
        return (transform.position.y < -15 || transform.position.y > 40 || transform.position.x > 40 || transform.position.x < -40);    //If the player is not within a box, they're out of bounds and dies
    }

    public void Move(float speed)   //Standard Movement
    {
        if (onGround)   //Check if on ground
        {
            //Accelerate the current speed
            body.velocity = new Vector3(body.velocity.x + speed * Input.GetAxis(horizontalAxis), body.velocity.y, body.velocity.z);
        }
    }





}
