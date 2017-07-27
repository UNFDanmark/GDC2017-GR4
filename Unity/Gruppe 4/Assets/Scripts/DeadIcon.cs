using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeadIcon : MonoBehaviour
{
    public Image alive;
    public Image dead;
    private Vector2 imgPos;

    void Awake()
    {
        imgPos = alive.transform.position;
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetDeadState (bool state)
    {
        if (state)
        {
            alive.transform.position = imgPos;
            dead.transform.position = new Vector2(-500, -500);
        }
        else
        {
            alive.transform.position = new Vector2(-500, -500);
            dead.transform.position = imgPos;
        }
    }
}
