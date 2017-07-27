using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    float time = 0;                                     // Time since start.

    // Scene 1.
    public Animator[] kunais;
    public float kunaisEntryTime = 1.0f;
    public float kunaisOffsetTime = 0.1f;

    
    [Header("Scene 2")]
    // Scene 2.
    public Animator shurikenEntry;
    public Animator shurikenRotation;
    public Animator buttonsFadein;

    public float shurikenEntryTime = 2.0f;
    public float shurikenRotationTimeOffset = 0.2f;
    public float buttonsFadeinOffset = 0.2f;

    [Header("Scene 3")]
    // Scene 3.
    public Animator controlsFadein;
    public float controlFadeintime;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Increment time.
        time += Time.deltaTime;

        


        // Scene 1.
        if (time >= shurikenEntryTime)
        {
//            shurikenRotation.SetBool("Rotate", true);
            shurikenEntry.SetBool("Start", true);
        }
        if (time >= shurikenEntryTime + shurikenRotationTimeOffset)
        {
//            shurikenRotation.SetBool("Rotate", false);
        }


        // Scene 2.
        if (time >= kunaisEntryTime)
        {
            kunais[0].SetBool("Shoot", true);
        }
        if (time >= kunaisEntryTime + kunaisOffsetTime)
        {
            kunais[1].SetBool("Shoot", true);
        }
        if (time >= kunaisEntryTime + kunaisOffsetTime * 2)
        {
            kunais[2].SetBool("Shoot", true);
        }
        if (time >= kunaisEntryTime + kunaisOffsetTime * 3)
        {
            kunais[3].SetBool("Shoot", true);
        }
        if (time >= kunaisEntryTime + kunaisOffsetTime*3 + buttonsFadeinOffset)
        {
            buttonsFadein.SetBool("Start", true);
        }

        // Scene 3.
        if (time >= controlFadeintime) {
            controlsFadein.SetBool("Start", true);
        }
        
    }
}
