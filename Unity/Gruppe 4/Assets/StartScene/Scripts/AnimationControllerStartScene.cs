using UnityEngine;
using System.Collections;

public class AnimationControllerStartScene : MonoBehaviour {

    float time = 0;                                     // Time since start.
    public AudioController speaker;

    [Header("Scene 1")]
    // Scene 1.
    public Animator shurikenEntry;
    public float shurikenEntryTime = 2.0f;
    [Header("Scene 2")]
    // Scene 2.
    public Animator[] kunais;
    public Animator buttonsFadein;
    public float kunaisEntryTime = 1.0f;
    public float kunaisOffsetTime = 0.1f;
    public float buttonsFadeinOffset = 0.2f;
    [Header("Scene 3")]
    // Scene 3.
    public Animator controlsFadein;
    public float controlFadeintime;


    // Use this for initialization
    void Start () {
        Cursor.visible = true;

    }
	
	// Update is called once per frame
	void Update () {
        // Increment time.
        time += Time.deltaTime;


        // Scene 1.
        if (time >= shurikenEntryTime)
        {
            shurikenEntry.SetBool("Start", true);
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
