using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public GlobalVariables globalVariables;

    [Header("Buttons")]
    public GameObject Options;
    public GameObject Credits;
    public GameObject ExtraCredits;

    [Header("Options")]
    public Slider sScore;
    public Slider sTime;
    public Toggle cPlatforms;
    public Toggle cHandicap;

    [Header("Text")]
    public Text tScore;
    public Text tTime;

    void Awake()
    {
        if (globalVariables == null)
        {
            globalVariables = GameObject.FindGameObjectWithTag("GLOBAL").GetComponent<GlobalVariables>();
        }
    }

    void Start() {
    }

    public void BStart()
    {
        // TODO: INSERT SOMETHING HERE MY FRIEND
        SceneManager.LoadScene("Dojo");
    }
    public void BOptions()
    {
        Options.SetActive(true);
        SetOptions();
    }
    public void BCredits()
    {
        Credits.SetActive(true);
    }
    public void BExtraCredits() 
    {
        ExtraCredits.SetActive(true);
    }
    public void BQuit() {
        Application.Quit();
    }
    public void BBack() {
        Options.SetActive(false);
        Credits.SetActive(false);
    }
    public void BBackExtra() {
        ExtraCredits.SetActive(false);
    }

    public void SScore() {
        Debug.Log(sScore.value);
        WriteText(tScore, FixScore(sScore.value));
    }
    public void STime() {
        Debug.Log(sTime.value);
        WriteText(tTime, FixTime(sTime.value));
    }

    public void CPlatforms()
    {
        globalVariables.platforms = cPlatforms.isOn;
    }
    public void CHandicap() {
        globalVariables.handicap = cHandicap.isOn;
    }

    void SetOptions()
    {
        sScore.value = ((float)globalVariables.scoreLimit) / 100;
        sTime.value = ((float)globalVariables.timeLimit) / 5400;

        cHandicap.isOn = globalVariables.handicap;
        cPlatforms.isOn = globalVariables.platforms;

        WriteText(tScore, FixScore(sScore.value));
        WriteText(tTime, FixTime(sTime.value));
    }

    void WriteText(Text text, string s) {
        text.text = s;
    }

    string FixScore(float value) {
        int v = (int)Mathf.Round(value * 100);
        globalVariables.scoreLimit = v;
        globalVariables.scored = v > 0;
        string s = v.ToString();
        if(!globalVariables.scored)
        {
            s = "\u221E";
        }
        return s;
    }

    string FixTime(float value)
    {
        string s;
        int v = (int)Mathf.Round(value * 90);
        globalVariables.timeLimit = v * 60;
        globalVariables.timed = v > 0;
        if (!globalVariables.timed)
        {
            s = "\u221E";
        }
        else {
            s = v.ToString() + " min";
        }
        return s;
    }

}

