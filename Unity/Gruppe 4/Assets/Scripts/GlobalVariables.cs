using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public int[] score = { 0, 0 };
    public bool timed = true;
    public bool scored = true;
    public int timeLimit = 5;
    public int scoreLimit = 10;
    public bool handicap = false;
    private bool active = false;
    public float gameTime;

    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("GLOBAL").Length > 1 && !active)
        {
            Destroy(gameObject);
        }
        
        active = true;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Dojo");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
