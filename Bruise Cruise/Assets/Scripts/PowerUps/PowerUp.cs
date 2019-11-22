﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected bool on = false;
    protected bool sick = false;
    protected float timer;
    protected float alcoholContent;
    private const float DRINK_INCREASE = 5;
    private const float CONTENT_MAX = 100;
    [SerializeField]
    protected int maxTime;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected ItemSystem itemSystem;

    private void Awake()
    {
        alcoholContent = 0f;
    }
    private void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            on = false;
        }
    }

    public void StartPowerUp()
    {
        
        // add item
        itemSystem.addPowerUp(sprite);
        //disable mesh renderer, collider 
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Use()
    {
        on = true;
        timer = maxTime;
        alcoholContent += DRINK_INCREASE;
        if(alcoholContent >= CONTENT_MAX) 
        {
            alcoholContent = 0;
            //barf animation
            sick = true;
            Debug.Log("BLEEUGGHH");
        }
    }
}

