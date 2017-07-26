using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    public PlayerBehaviour player;
    public Animator am;

    bool isCharching = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCharging();

	}

    void UpdateCharging()
    {
        bool _carching = player.charging;
        if(isCharching != _carching)
        {
            if (_carching)
            {
                // Charging stated.
                am.SetBool("StartCharge", true);
            }
            else
            {
                // Charging Stopped.
                am.SetBool("StartCharge", false);
            }
        }
        isCharching = _carching;

    }
}
