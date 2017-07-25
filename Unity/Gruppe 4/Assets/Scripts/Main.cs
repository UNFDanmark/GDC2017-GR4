using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public int[] score = { 0, 0 };
    public GameObject[] player = new GameObject[2];
    private PlayerBehaviour[] playerBehaviour;
    public string[] playerName = { "P1", "P2" };

    void Awake()
    {
        playerBehaviour = new PlayerBehaviour[player.Length];
        for (int i = 0; i < playerBehaviour.Length; i++)
        {
            playerBehaviour[i] = player[i].GetComponent<PlayerBehaviour>();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void KOD(GameObject victim, PlayerBehaviour victimScript)
    {
        int deadIndex = 0;
        bool found = false;
        {
            for (int i = 0; i < player.Length && !found; i++)
            {
                if (player[i].Equals(victim) && playerBehaviour[i].Equals(victimScript))
                {
                    found = true;
                    deadIndex = i;
                }
            }
        }
        score[1 - deadIndex]++;
        print(score[0] + " ; " + score[1]);
    }
}
