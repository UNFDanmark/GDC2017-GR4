using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{

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

    void Start() {
    }

    public void BStart()
    {
        // TODO: INSERT SOMETHING HERE MY FRIEND
    }
    public void BOptions()
    {
        Options.SetActive(true);
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
        Debug.Log(cPlatforms.isOn);
    }
    public void CHandicap() {
        Debug.Log(cHandicap.isOn);
    }

    void SetOptions()
    {
        sScore.value = 0;
        sTime.value = 0.03f;

        cHandicap.isOn = false;
        cPlatforms.isOn = true;

        WriteText(tScore, FixScore(sScore.value));
        WriteText(tTime, FixTime(sTime.value));
    }

    void WriteText(Text text, string s) {
        text.text = s;
    }

    string FixScore(float value) {
        int v = (int)(value * 100);
        string s = v.ToString();
        return s;
    }

    string FixTime(float value)
    {
        int v = (int)(value * 100);
        string s = v.ToString() + " s";
        Debug.Log(s);
        return s;
    }

}

