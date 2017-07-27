using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinnerScreenControlller : MonoBehaviour
{
    public GlobalVariables globalVariables;

    public bool winnerIsBlue = false;
    public int blueKills = 19;
    public int redKills = 78;

    [Header("References")]
    public Image blueHead;
    public Image redHead;
    public SpriteRenderer backGround;

    public GameObject blueCharacter;
    public GameObject redCharacter;
    public GameObject confetti;

    [Header("Blue")]
    public Sprite blueHeadWinner;
    public Sprite blueHeadLoser;
    public Sprite blueBackground;
    public GameObject blueKillList;
    public Material blueConfetti;

    [Header("Red")]
    public Sprite redHeadWinner;
    public Sprite redHeadLoser;
    public Sprite redBackground;
    public GameObject redKillList;
    public Material redConfetti;

    void Awake()
    {
        Cursor.visible = true;
        globalVariables = GameObject.FindGameObjectWithTag("GLOBAL").GetComponent<GlobalVariables>();
    }

    void Start()
    {
        GetValues();
        SetColors();
    }

    public void BMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void BRematch()
    {
        SceneManager.LoadScene("Dojo");
    }

    // Finds all data needed.
    void GetValues()
    {
        redKills = globalVariables.score[0];
        blueKills = globalVariables.score[1];

        winnerIsBlue = blueKills > redKills;
    }

    // Sets colors.
    void SetColors()
    {

        if (winnerIsBlue)
        {
            // Blue Winner.
            // Blue.
            blueHead.sprite = blueHeadWinner;
            backGround.sprite = blueBackground;
            blueCharacter.SetActive(true);
            confetti.GetComponent<ParticleSystemRenderer>().material = blueConfetti;

            // Red.
            redHead.sprite = redHeadLoser;
            redCharacter.SetActive(false);
        }
        else
        {
            // Red Winner.
            // Red.
            redHead.sprite = redHeadWinner;
            backGround.sprite = redBackground;
            redCharacter.SetActive(true);
            confetti.GetComponent<ParticleSystemRenderer>().material = redConfetti;

            // Blue.
            blueHead.sprite = blueHeadLoser;
            blueCharacter.SetActive(false);
        }

        SetKills(blueKillList, blueKills);
        SetKills(redKillList, redKills);

    }

    // Setup kill list.
    void SetKills(GameObject killList, int amount)
    {
        // Reset icons.
        for (int i = 0; i < killList.transform.childCount; i++)
        {
            Transform child = killList.transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(false);
            }
        }

        // Set icons.
        int set = 0;
        if (amount == 0)
            return;

        for (int i = 0; i < killList.transform.childCount; i++)
        {
            Transform child = killList.transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
                set++;
                if (set == amount)
                    return;
            }
        }
        

    }
}
