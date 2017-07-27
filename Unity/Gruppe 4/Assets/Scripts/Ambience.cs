using UnityEngine;
using System.Collections;

public class Ambience : MonoBehaviour {

    public AudioSource main;
    public AudioSource second;

    float audioStartTime;

    public float loopTime;
    public float volume;
    
	// Use this for initialization
	void Start () {
        main.volume = volume;
        second.volume = volume;
        main.Play();
        audioStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time - audioStartTime > loopTime)
        {
            AudioSource temp = main;
            main = second;
            main.Play();

            second = temp;

            audioStartTime = Time.time;
        }
	}
}
