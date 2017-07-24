using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour {

    public GameObject player;
    public Image arrow;
    public PlayerBehaviour playerBehaviour;

    void Awake()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 screenPosition = Camera.current.WorldToScreenPoint(player.transform.position);
        transform.position = screenPosition;
        transform.eulerAngles = new Vector3(0, 0, playerBehaviour.chargeDir);
	}
}
