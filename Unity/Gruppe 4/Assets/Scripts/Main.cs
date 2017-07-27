﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int[] score = { 0, 0 };
    public GameObject[] player = new GameObject[2];
    private PlayerBehaviour[] playerBehaviour;
    public string[] playerName = { "P1", "P2" };
    public Vector3[] respawnPoints;
    public float respawnTime = 2.5f;
    public Image[] playerUI = new Image[2];
    private GlobalVariables globalVariables;
    public float handicapMod = 0.5f;

    void Awake()
    {
        playerBehaviour = new PlayerBehaviour[player.Length];
        for (int i = 0; i < playerBehaviour.Length; i++)
        {
            playerBehaviour[i] = player[i].GetComponent<PlayerBehaviour>();
        }
        if(globalVariables == null)
        {
            globalVariables = GameObject.FindGameObjectWithTag("GLOBAL").GetComponent<GlobalVariables>();
        }
    }

	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < player.Length; i++)
        {
            Respawn(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < player.Length; i++)
        {

            // Find pos.
            // Spawn smoke.
            // Wait 0.5 sek.
            // Spawn player.

            if(!playerBehaviour[i].alive && Time.time - playerBehaviour[i].timeOfDeath >= respawnTime)
            {
                Respawn(i);
            }
        }
	
	}


    public void KOD(GameObject victim, PlayerBehaviour victimScript)
    {
        //ADD SOUND EFFECT HERE!!

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

        player[deadIndex].transform.position = new Vector3(0, -100, 0);
        playerBehaviour[deadIndex].timeOfDeath = Time.time;
        playerBehaviour[deadIndex].alive = false;
        playerUI[deadIndex].GetComponent<DeadIcon>().SetAliveState(false);

        int ahead = 0;
        int highestScore = 0;
        for(int i = 0; i < score.Length; i++)
        {
            if(score[i] > highestScore)
            {
                highestScore = score[i];
            }
            ahead = i;
        }

        if (score[1 - ahead] <= highestScore - 5)
        {
            playerBehaviour[1 - ahead].maxEnergy = playerBehaviour[1 - ahead].baseMaxEnergy + Mathf.Floor((highestScore - score[1 - ahead]) / 5);
        }
        else
        {
            playerBehaviour[1 - ahead].maxEnergy = playerBehaviour[1 - ahead].baseMaxEnergy;
        }

    }

    public void Respawn(int deadIndex)
    {
        player[deadIndex].SetActive(true);
        int attempts = 0;
        int spawnPoint = Random.Range(0, respawnPoints.Length);
        while ((respawnPoints[spawnPoint] - player[1 - deadIndex].transform.position).magnitude < 10 && attempts <= 25)
        {
            spawnPoint = Random.Range(0, respawnPoints.Length);
            attempts++;
        }
        playerBehaviour[deadIndex].alive = true;
        player[deadIndex].transform.position = respawnPoints[spawnPoint];
        playerBehaviour[deadIndex].Respawn();
        playerUI[deadIndex].GetComponent<DeadIcon>().SetAliveState(true);

        playerBehaviour[deadIndex].sound.RespawnSound();
    }
}
