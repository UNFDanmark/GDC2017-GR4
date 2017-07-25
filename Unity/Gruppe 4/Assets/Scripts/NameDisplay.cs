using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameDisplay : MonoBehaviour {
    public GameObject main;
    public int playerIndex = 0;
    Text text;
    private Main mainScript;

    void Awake()
    {
        mainScript = main.GetComponent<Main>();
    }

	// Use this for initialization
	void Start () {
        SetPlayerName(mainScript.playerName[playerIndex]);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void SetPlayerName(string name)
    {
        GetComponent<Text>().text = name;
    }
}
