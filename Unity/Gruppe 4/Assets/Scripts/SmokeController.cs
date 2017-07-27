using UnityEngine;
using System.Collections;

public class SmokeController : MonoBehaviour {

    float time = 0;
    public float timeAlive = 1.0f;

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if(time > timeAlive)
        {
            Destroy(transform.gameObject);
        }
	}
}
