using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Image[] minutes = new Image[2];
    public Image[] seconds = new Image[2];
    public Image colon;
    public Image SuddenDeath;

    public Sprite[] numbers = new Sprite[10];

    private Main main;

    private Vector2 suddenStart;

    void Awake()
    {
        suddenStart = SuddenDeath.transform.position;
        SuddenDeath.transform.position = new Vector2(0, -200);
        main = gameObject.GetComponent<Main>();
    }

    // Use this for initialization
    void Start ()
    {
	    if(!main.globalVariables.timed || main.suddenDeath)
        {
            for (int i = 0; i < 2; i++)
            {
                minutes[i].transform.position = new Vector2(0, -200);
                seconds[i].transform.position = new Vector2(0, -200);
            }
            colon.transform.position = new Vector2(0, -200);
            
        }
        setNumbers();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!main.globalVariables.timed || main.suddenDeath)
        {
            for (int i = 0; i < 2; i++)
            {
                minutes[i].transform.position = new Vector2(0, -200);
                seconds[i].transform.position = new Vector2(0, -200);
            }
            colon.transform.position = new Vector2(0, -200);
            if (main.suddenDeath && main.globalVariables.timed)
            {
                SuddenDeath.transform.position = suddenStart;
            }
        }
        else
        {
            setNumbers();
        }
    }


    public void setNumbers()
    {
        float timeRemaining = Mathf.Max(main.globalVariables.timeLimit - (Time.time - main.gameStart), 0);

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
