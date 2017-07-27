using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public float[] score = { 0, 0 };
    public bool timed = true;
    public bool infinite = false;
    public int timeLimit = 5;
    public int scoreLimit = 10;
    public bool handicap = false;
    private bool active = false;

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
