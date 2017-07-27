using UnityEngine;
using System.Collections;

public class NoPlatforms : MonoBehaviour {
    public GameObject main;
    private Main mainScript;
    // Use this for initialization
    void Awake()
    {
        mainScript = main.GetComponent<Main>();
    }

    void Start () {
	    if(!mainScript.globalVariables.platforms)
        {
            Destroy(gameObject);
        }
	}
}
