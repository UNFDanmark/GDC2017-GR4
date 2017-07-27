using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    public int[] score = { 0, 0 };
    public GameObject[] player = new GameObject[2];
    private PlayerBehaviour[] playerBehaviour;
    public string[] playerName = { "P1", "P2" };
    public Vector3[] respawnPoints;
    public Vector3[] platformRespawn;
    public Vector3[] noPlatformRespawn;
    public float respawnTime = 2.5f;
    public Image[] playerUI = new Image[2];
    public GlobalVariables globalVariables;
    public float gameStart = 0;
    public bool suddenDeath = false;

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
        if(globalVariables.platforms)
        {
            respawnPoints = platformRespawn;
        }
        else
        {
            respawnPoints = noPlatformRespawn;
        }
        for(int i = 0; i < player.Length; i++)
        {
            Respawn(i);
        }
        gameStart = Time.time;
        for (int i = 0; i < globalVariables.score.Length; i++)
        {
            globalVariables.score[i] = 0;
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
        if (Time.time - gameStart > globalVariables.timeLimit && globalVariables.timed)
        {
            if (score[0] != score[1])
            {
                globalVariables.score = score;
                globalVariables.gameTime = Time.time - gameStart;
                SceneManager.LoadScene("VictoryScreen");
            }
            else if (!suddenDeath)
            {
                for (int p = 0; p < player.Length; p++)
                {
                    playerBehaviour[p].pushForce *= 6;
                    Respawn(p);
                }
                suddenDeath = true;
            }
        }

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
                ahead = i;
            }
        }

        if (score[1 - ahead] <= highestScore - 5)
        {
            playerBehaviour[1 - ahead].maxEnergy = playerBehaviour[1 - ahead].baseMaxEnergy + Mathf.Floor((highestScore - score[1 - ahead]) / 5);
        }
        else
        {
            playerBehaviour[1 - ahead].maxEnergy = playerBehaviour[1 - ahead].baseMaxEnergy;
        }

        if(globalVariables.scored && highestScore >= globalVariables.scoreLimit || suddenDeath)
        {
            globalVariables.score = score;
            globalVariables.gameTime = Time.time - gameStart;
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            print("HELLO");
            playerBehaviour[deadIndex].sound.DeathSound();
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
