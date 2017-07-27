using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class PlayerSounds : MonoBehaviour {

    public AudioClip[] dash;
    public AudioClip[] hitGround;
    public AudioClip[] hitPlayer;
    public AudioClip[] dashCharge;
    public AudioClip[] deathSound;
    public AudioClip[] respawnSound;

    public new AudioSource[] audio; //should have as many as there are unique sounds to play in parallel

    public void Dash()
    {
        audio[0].volume = 0.5f;
        Play(dash,0);
    }

    public void HitGround(float velocity)
    {
        //volume increases as velocity increases
//        Debug.Log("hit ground with velocity " + velocity);
        if (velocity <= 1.3f) return;

        audio[1].volume = 1;
        Play(hitGround,1);
    }

    public void HitPlayer(float velocity)
    {
//        Debug.Log("Hit player with velocity " + velocity);
        //TODO calc volume
        audio[2].volume = 1;
        Play(hitPlayer,2);
    }

    public void ChargeDash()
    {
        audio[3].volume = 0.21f;
        Play(dashCharge, 3);
    }

    public void CancelDashCharge()
    {
        audio[3].Stop();
    }

    public void DeathSound()
    {
        audio[4].volume = 0.7f;
        Play(deathSound, 4);
    }
    
    public void RespawnSound()
    {
        audio[4].volume = 1;
        Play(respawnSound, 4);
    }


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
