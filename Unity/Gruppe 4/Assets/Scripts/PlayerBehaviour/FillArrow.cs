using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillArrow : MonoBehaviour
{

    public GameObject player;
    public Image arrow;
    public PlayerBehaviour playerBehaviour;

    void Awake()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerBehaviour.charging && playerBehaviour.chargeDir >= 0)
        {
            arrow.fillAmount = Mathf.Min(Time.time - playerBehaviour.chargeStart, playerBehaviour.chargeMax) / playerBehaviour.chargeMax;
        }
        else
        {
            transform.position = new Vector3(-250, -250, 0);
        }
    }
}
