using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public GameObject main;
    public int playerIndex = 0;
    private Main mainScript;
    public Image[] ciphers = new Image[4];
    public bool left = true;

    void Awake()
    {
        mainScript = main.GetComponent<Main>();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        string textToDisplay = "Score: ";
        if (mainScript.score[playerIndex] > 999999)
        {
            textToDisplay += "A LOT";
        }
        else
        {
            textToDisplay += mainScript.score[playerIndex];
        }
        GetComponent<Text>().text = textToDisplay;
	}
}
