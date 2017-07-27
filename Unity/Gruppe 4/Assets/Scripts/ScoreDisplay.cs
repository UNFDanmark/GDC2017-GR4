using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public GameObject main;
    public int playerIndex = 0;
    private Main mainScript;
    public Image[] ciphers = new Image[4];
    public bool left = true;
    public Sprite empty;
    public Sprite[] numbers = new Sprite[10];

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
        if (mainScript.score[playerIndex] > 9001)
        {
            ciphers[0].sprite = numbers[1];
            ciphers[1].sprite = numbers[0];
            ciphers[2].sprite = numbers[0];
            ciphers[3].sprite = numbers[9];
            return;
        }
        int cipherCount = (int)Mathf.Max(Mathf.Min(Mathf.Floor(Mathf.Log10(mainScript.score[playerIndex])) + 1, ciphers.Length), 1);

        if (left)
        {
            for (int i = 0; i < cipherCount; i++)
            {
                ciphers[i].sprite = numbers[(int)Mathf.Floor(mainScript.score[playerIndex] / Mathf.Pow(10, i)) % 10];
            }
            for (int i = cipherCount; i < ciphers.Length; i++)
            {
                ciphers[i].sprite = empty;
            }
        }
        else
        {
            for(int i = ciphers.Length - 1; i >= ciphers.Length - cipherCount; i--)
            {
                ciphers[i].sprite = numbers[(int)(Mathf.Floor(mainScript.score[playerIndex] / Mathf.Pow(10, i + cipherCount - ciphers.Length)) % 10)];
            }
            for(int i = ciphers.Length - cipherCount - 1; i >= 0; i--)
            {
                ciphers[i].sprite = empty;
            }
        }
	}
}
