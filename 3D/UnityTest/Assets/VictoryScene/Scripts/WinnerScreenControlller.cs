using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinnerScreenControlller : MonoBehaviour {

    bool winnerIsBlue = false;

    [Header("References")]
    public Image blueHead;
    public Image redHead;
    public SpriteRenderer backGround;

    public GameObject character;

    [Header("Blue")]
    public Sprite blueHeadWinner;
    public Sprite blueHeadLoser;
    public Sprite blueBackground;
    public GameObject blueCharacter;
    public GameObject blueKillList;
    public Texture blueConfetti;

    [Header("Red")]
    public Sprite redHeadWinner;
    public Sprite redHeadLoser;
    public Sprite redBackground;
    public GameObject redCharacter;
    public GameObject redKillList;
    public Texture redConfetti;


    public void BMenu() {
        // MENU
    }
    public void BRematch() {
        // DOJO
    }

    // Blue Winner.
    void BlueWinner() {
        
    }


}
