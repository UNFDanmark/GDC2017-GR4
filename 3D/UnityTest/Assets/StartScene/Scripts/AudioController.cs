using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

    public AudioSource audioSourceOne;
    public AudioSource audioSourceTwo;
    public List<AudioClip> clips;
    Dictionary<string, AudioClip> sounds;

    public float ambientVolume = 0.5f;

    private void Start() {
        sounds = new Dictionary<string, AudioClip>();
        for (int i = 0; i < clips.Count; i++)
        {
            sounds.Add(clips[i].name, clips[i]);
        }

        // Start bagground music.
        Play("main menu ambience");
    }

    void Update() {
        Mathf.Clamp01(ambientVolume);
    }

    public void Play(string name) {
        if (!audioSourceOne.isPlaying)
        {
            SetVolume(audioSourceOne, name);
            audioSourceOne.clip = sounds[name];
            audioSourceOne.Play();
        }
        else 
        {
            SetVolume(audioSourceTwo, name);
            audioSourceTwo.clip = sounds[name];
            audioSourceTwo.Play();
        }
    }

    void SetVolume(AudioSource audioS, string name) {
        if (name == "main menu ambience")
            audioS.volume = ambientVolume;
        else
            audioS.volume = 1;
    }

}
