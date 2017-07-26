using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class PlayerSounds : MonoBehaviour {

    public AudioClip[] dash;
    public AudioClip[] hitGround;
    public AudioClip[] hitPlayer;
    public AudioClip[] dashCharge;

    public new AudioSource[] audio; //should have as many as there are unique sounds to play in parallel

    public void Dash()
    {
        audio[0].volume = 0.5f;
        Play(dash,0);
    }

    public void HitGround(float velocity)
    {
        //volume increases as velocity increases
        //Debug.Log("hit ground with velocity " + velocity);

        audio[1].volume = velocity * 0.5f;
        Play(hitGround,1);
    }

    public void HitPlayer(float velocity)
    {
        Debug.Log("Hit player with velocity " + velocity);
        //TODO calc volume
        audio[2].volume = velocity * 0.5f;
        Play(hitPlayer,2);
    }

    public void ChargeDash()
    {
        audio[3].volume = 0.3f;
        Play(dashCharge, 3);
    }

    public void CancelDashCharge()
    {
        audio[3].Stop();
    }

    //TODO special situation for dashCharge (can be cancelled!)
    //TODO special situation for ambience (loops, should be put in a different script!)
    


    //generic play function - is called in specific audio settings
    private void Play(AudioClip[] sound, int i)
    {
        if (sound.Length == 0) return;
        audio[i].clip = sound[Random.Range(0, sound.Length)];
        audio[i].Play();
    }
    
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
