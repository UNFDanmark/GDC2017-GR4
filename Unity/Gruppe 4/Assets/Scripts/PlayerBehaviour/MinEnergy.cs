﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinEnergy : MonoBehaviour {
    public Image image;
    public GameObject player;
    private PlayerBehaviour playerBehaviour;

    void Awake()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

	// Use this for initialization
	void Start () {
        SetHeight();
	}
	
	// Update is called once per frame
	void Update () {
        SetHeight();
    }

    //SPLIT

    public void SetHeight()
    {
        transform.localScale = new Vector3(transform.localScale.x, playerBehaviour.energyCostMin / playerBehaviour.maxEnergy, 1);
    }
}
