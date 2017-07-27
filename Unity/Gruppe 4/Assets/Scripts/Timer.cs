using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Image[] minutes = new Image[2];
    public Image[] seconds = new Image[2];

    public Sprite[] numbers = new Sprite[10];

    private Main main;

    void Awake()
    {
        main = gameObject.GetComponent<Main>();
    }

    // Use this for initialization
    void Start ()
    {
	    if(!main.globalVariables.timed)
        {
            for (int i = 0; i < 2; i++)
            {
                minutes[i].transform.position = new Vector2(0, -200);
                seconds[i].transform.position = new Vector2(0, -200);
            }
        }
        setNumbers();

    }
	
	// Update is called once per frame
	void Update ()
    {
        setNumbers();

    }


    public void setNumbers()
    {
        float timeRemaining = main.globalVariables.timeLimit - (Time.time - main.gameStart);

        int sec = (int) Mathf.Floor(timeRemaining % 60);

        int s10 = (int)Mathf.Floor(sec / 10);
        int s1 = sec % 10;

        seconds[0].sprite = numbers[s10];
        seconds[1].sprite = numbers[s1];

        int min = (int)Mathf.Floor(timeRemaining / 60);

        int m10 = (int)Mathf.Floor(min / 10);
        int m1 = min % 10;

        minutes[0].sprite = numbers[m10];
        minutes[1].sprite = numbers[m1];
    }
}
