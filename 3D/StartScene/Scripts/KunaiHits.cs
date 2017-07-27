using UnityEngine;
using System.Collections;

public class KunaiHits : MonoBehaviour
{
    public AudioController speaker;
    bool played = false;

    private void Update()
    {
    // Will play a sound when it collides with the pole.
        if (!played)
            if (transform.position.x <= -9)
            {
                speaker.Play("kunai hit sound");
                played = true;
            }
    }
}
