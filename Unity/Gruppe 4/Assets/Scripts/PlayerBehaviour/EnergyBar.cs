using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    public GameObject player;
    private PlayerBehaviour playerBehaviour;
    public Image bar;

    void Awake()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        bar.fillAmount = playerBehaviour.energy / playerBehaviour.maxEnergy;
	}
}
