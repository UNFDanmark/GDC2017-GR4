using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public bool mainPlayer = false;
    public string horizontalAxis;   //The axis used for moving left and right and for aiming
    public string verticalAxis; //Another axis used for aiming
    public string chargeDashAxis;   //The axis used for the inputs to charge and execute a dash
    [Space(20)]
    public float maxEnergy = 2;
    public float energyCostRate = 1;
    public float energyCostMin = 0.1f;
    public float energyRegenRate = 2;
    public float gracePeriod = 0.1f;
    public float airChargeRate = 0.05f;
    [Space(10)]
    public float chargeMax = 0.9f; //The maximal amount of time in which the player can charge
    public float chargeMin = 0.15f; //A bonus length aplied to all charges
    public float pushForce = 10;    //The amount of force with which the player pushes other objects
    public float walkSpeed = 1; //The amount of speed with which the player "walks"
    public float dashForce = 25;    //The amount of force with which the player dashes
    public float chargeParachuteFactor = 2;
    [Space(20)]
    public Rigidbody body;  //The object's Rigidbody
    public Collider collision;  //The object's collider
    public GameObject main;
    [Space(20)]
    public Vector3 spawn; //The spawn location for the player
    public float chargeDir = 90; //The direction in which the currently charging dash is pointed
    [Space(20)]
    public bool charging = false;   //A boolean that keeps track of whether the player is charging
    public bool onGround = false;   //A boolean that keeps track of whether the player is on the ground
    public float chargeStart = 0;   //A boolean that keeps track of when the player started charging their dash
    public float energy;
    public float timeOfLastTouch = 0;
    public float timeOfLastDash = 0;


    public PlayerSounds sound;

    
    void Awake ()
    {
        body = GetComponent<Rigidbody>();   //Sets the Rigidbody
        spawn = transform.position; //Sets the spawn position to the start position
        energy = maxEnergy;
        timeOfLastTouch = 0;
        timeOfLastDash = 0;
    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Charge();   //Run the charge command
        RegenerateEnergy();
    }

    void FixedUpdate()  //Runs 50 times per second
    {
        if (!charging)
        {
            Move(walkSpeed);    //Run the walk command with the object's walk speed
        }
        else 
        {
            if (body.velocity.y < 0)
            {
                body.velocity = (new Vector3(body.velocity.x, body.velocity.y / chargeParachuteFactor, body.velocity.z));
            }
        }

        if (KOCheck()) KO();    //Checks if the player is KO'd, and if they are, KO them
    }
    
    public void OnCollisionEnter(Collision col)    //Runs when initiating contact with other objects
    {
        if (col.gameObject.CompareTag(gameObject.tag))  //Checks if the other object's tag matches the current tag (is the object another player?)
        {
            if (mainPlayer)
            {
                Rigidbody other = col.gameObject.GetComponent<Rigidbody>(); //Stores the collision object's Rigidbody in other

                Vector2 colSpeed = body.velocity - other.velocity;

                sound.HitPlayer(colSpeed.magnitude);

                if (other.velocity.magnitude > body.velocity.magnitude) //Checks if the current object moves slower than the other object
                {
                    body.velocity = body.velocity + (body.position - other.position).normalized * pushForce * other.velocity.magnitude; //Shoots self away from the other player

                }
                else if (other.velocity.magnitude < body.velocity.magnitude)
                {
                    other.velocity = other.velocity + (other.position - body.position).normalized * pushForce * body.velocity.magnitude; //Shoots self away from the other player
                }

            }
        }
        else
        {
            sound.HitGround(System.Math.Abs(body.velocity.y));
        }
    }

    public void OnCollisionStay(Collision col) //Runs while in contact with other object
    {
        if (!col.gameObject.CompareTag(gameObject.tag)) //Checks if the tags DO NOT match
        {
            switch (col.gameObject.tag)
            {
                case "Arena":
                    onGround = true;    //Inform's the game that the player is on the ground
                    timeOfLastTouch = Time.time;
                    break;
            }
        }
    }

    public void OnCollisionExit(Collision col) //Runs when leaving contact with other objects
    {
        if (!col.gameObject.CompareTag(gameObject.tag)) //Checks if the tags DO NOT match
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
            if (!charging && energy >= energyCostMin)  //Check if already charging
            {
                sound.ChargeDash();
                chargeStart = Time.time;    //Set the start time of the charge
                charging = true;    //Inform the system that the charge has started
                
            }
            else
            {
                /*if (chargeDir > 0 && chargeDir < 180)
                {
                    chargeDir -= Input.GetAxis(horizontalAxis) * aimSensitivity * Time.deltaTime;
                }
                else if (chargeDir > 180 && chargeDir < 360)
                {
                    chargeDir += Input.GetAxis(horizontalAxis) * aimSensitivity * Time.deltaTime;
                }

                if (chargeDir > 90 && chargeDir < 270)
                {
                    chargeDir -= Input.GetAxis(verticalAxis) * aimSensitivity * Time.deltaTime;
                }
                else if (chargeDir < 90 || chargeDir > 270)
                {
                    chargeDir += Input.GetAxis(verticalAxis) * aimSensitivity * Time.deltaTime;
                }*/

                

                
            }
            chargeDir = SetChargeDirection((int)Mathf.Round(Input.GetAxis(horizontalAxis)), (int)Mathf.Round(Input.GetAxis(verticalAxis)));
        }
        else
        {
            chargeDir = SetChargeDirection((int)Mathf.Round(Input.GetAxis(horizontalAxis)), (int)Mathf.Round(Input.GetAxis(verticalAxis)));
            if (charging)   //Check if previously charging
            {
                Dash(); //Launch dash
                charging = false;   //Inform the system that the charge has ended
            }
        }
    }

    public void Dash()
    {
        sound.CancelDashCharge();
        if (energy >= energyCostMin && chargeDir >= 0)
        {
            sound.Dash();
            energy -= energyCostMin;
            float chargeTime = Mathf.Min(chargeMax, Time.time - chargeStart, energy / energyCostRate);
            energy -= chargeTime * energyCostRate;
            
            chargeTime += chargeMin;
            
            body.velocity = body.velocity + new Vector3(Mathf.Cos(chargeDir / 180 * Mathf.PI) * dashForce * chargeTime, Mathf.Sin(chargeDir / 180 * Mathf.PI) * dashForce * chargeTime, 0);
            timeOfLastDash = Time.time;
        }
    }

    public void KO()    //KO's a player and respawns them
    {
        transform.position = spawn; //Sets the player's current position to the spawn position
        body.velocity = new Vector3(0, 0, 0);   //Sets the current speed to be 0
        body.angularVelocity = new Vector3(0, 0, 0);    //Stops the current rotation
        energy = maxEnergy;

        //======================================================================================

        main.GetComponent<Main>().KOD(gameObject, this);
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

    public void RegenerateEnergy()
    {
        if (Time.time - timeOfLastDash >= gracePeriod)
        {
            if (Time.time - timeOfLastTouch <= gracePeriod)
            {
                energy = Mathf.Min(energy + energyRegenRate * Time.deltaTime, maxEnergy);
            }
            else
            {
                energy += airChargeRate * Time.deltaTime;
            }
        }
    }

    public float SetChargeDirection(int x, int y)
    {
        switch (x)
        {
            case -1:
                switch(y)
                {
                    case -1:
                        return (225);
                    case 0:
                        return (180);
                    case 1:
                        return (135);
                    default:
                        return (180);
                }
            case 0:
                switch (y)
                {
                    case -1:
                        return (270);
                    case 0:
                        return (-1);
                    case 1:
                        return (90);
                    default:
                        return (-1);
                }
            case 1:
                switch (y)
                {
                    case -1:
                        return (315);
                    case 0:
                        return (0);
                    case 1:
                        return (45);
                    default:
                        return (0);
                }
            default:
                return (-1);
        }
    }

}
