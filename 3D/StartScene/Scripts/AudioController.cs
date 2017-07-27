using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

    public AudioSource audioSource;
    public List<AudioClip> clips;
    Dictionary<string, AudioClip> sounds;

    private void Start() {
        sounds = new Dictionary<string, AudioClip>();
        for (int i = 0; i < clips.Count; i++)
        {
            sounds.Add(clips[i].name, clips[i]);
        }
    }

    public void Play(string name) {
        audioSource.clip = sounds[name];
        audioSource.Play();

    }

}
