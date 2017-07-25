using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class PlayerSounds : MonoBehaviour {

    public AudioClip[] dash;
    public AudioClip[] hitGround;
    public AudioClip[] hitPlayer;
    public AudioClip[] dashCharge;

    public new AudioSource audio;

    public void Dash()
    {
        audio.volume = 1;
        Play(dash);
    }

    public void HitGround(float velocity)
    {
        CalcVolume(velocity);
        //volume increases as velocity increases
        audio.volume = 3;
        Play(hitGround);
    }

    public void HitPlayer(float velocity)
    {
        CalcVolume(velocity);
        audio.volume = 1;
        Play(hitPlayer);
    }

    //TODO special situation for dashCharge (can be cancelled!)
    //TODO special situation for ambience (loops, should be put in a different script!)
    
    private float CalcVolume(float velocity)
    {
        Debug.Log("Velocity at collision: " + velocity);
        return 1;
    }

    //generic play function - is called in specific audio settings
    private void Play(AudioClip[] sound)
    {
        if (sound.Length == 0) return;
        audio.clip = sound[Random.Range(0, sound.Length)];
        audio.Play();
    }
    
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
