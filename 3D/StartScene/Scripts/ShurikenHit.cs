using UnityEngine;
using System.Collections;

public class ShurikenHit : MonoBehaviour {

    public AudioController speaker;
    bool played = false;

    void Update() 
    { 
        if(!played)
            if(transform.position.x >= 5) 
                {
                    speaker.Play("shuriken hit sound");
                    played = true;
                }
    }


}
