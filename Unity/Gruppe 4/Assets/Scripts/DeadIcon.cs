using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeadIcon : MonoBehaviour
{

    public Sprite alive;
    public Sprite dead;
    public Image image;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetAliveState (bool state)
    {
        if(state)
        {
            image.sprite = alive;
        }
        else
        {
            image.sprite = dead;
        }
    }
}
