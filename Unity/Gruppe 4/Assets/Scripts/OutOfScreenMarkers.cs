using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OutOfScreenMarkers : MonoBehaviour {
    public GameObject player;
    public PlayerBehaviour playerScript;
    public Image image;
    public float arrowSize = 32;
    public Image icon;

    void Awake()
    {
        playerScript = player.GetComponent<PlayerBehaviour>();
    }

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        SetMarker();
	}

    public void SetMarker()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        if ((playerScreenPosition.x > Screen.width || playerScreenPosition.x < 0 || playerScreenPosition.y > Screen.height || playerScreenPosition.y < 0) && playerScript.alive)
        {
            transform.position = new Vector2(Mathf.Max(Mathf.Min(Screen.width - arrowSize, playerScreenPosition.x), arrowSize), Mathf.Max(Mathf.Min(Screen.height - arrowSize, playerScreenPosition.y), arrowSize));
            Vector2 toPlayer = playerScreenPosition - transform.position;
            transform.eulerAngles = new Vector3(0, 0, FindDirection(toPlayer));
            icon.transform.position = new Vector2(Mathf.Max(Mathf.Min(Screen.width - arrowSize, playerScreenPosition.x), arrowSize), Mathf.Max(Mathf.Min(Screen.height - arrowSize, playerScreenPosition.y), arrowSize));
        }
        else
        {
            transform.position = new Vector2(-200, -200);
            icon.transform.position = new Vector2(-200, -200);
        }
    }

    public float FindDirection(Vector2 vector)
    {
        float angle = Vector2.Angle(vector, Vector2.right);
        if (vector.y < 0) angle = 360 - angle;
        return(angle);
    }
}
