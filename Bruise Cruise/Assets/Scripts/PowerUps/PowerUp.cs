using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public bool on = false;
    protected bool sick = false;
    protected float timer;
    protected float alcoholContent;
    private const float DRINK_INCREASE = 20; // change to a smaller value, only this high when debugging.
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
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        } else if (alcoholContent > 0)
        {
            alcoholContent -= Time.deltaTime;
        }
        
    }

    public void StartPowerUp()
    {
        
        // add item
        itemSystem.addPowerUp(sprite);
        //disable mesh renderer, collider 
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        on = true;
    }

    public void Use()
    {
        alcoholContent += DRINK_INCREASE;
        timer = maxTime;
        if (alcoholContent >= CONTENT_MAX) 
        {
            alcoholContent = 0;
            //barf animation
            sick = true;
            Debug.Log("BLEEUGGHH");
        }
    }
}

