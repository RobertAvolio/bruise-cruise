using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public bool on = false;
    public PlayerMovement movement;
    public PlayerStats stats;
    protected bool sick = false;
    
    private const float DRINK_INCREASE = 50; // change to a smaller value, only this high when debugging.
    private const float CONTENT_MAX = 100;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected ItemSystem itemSystem;
    [SerializeField]
    private int maxTime;

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
        stats.alcoholContent += DRINK_INCREASE;
        
        stats.timer = maxTime;
        if (stats.alcoholContent >= CONTENT_MAX) 
        {
            stats.alcoholContent = 0;
            movement.cannot_move = true;
            movement.Vomit();
            // Debug.Log("BLEEUGGHH");
        }
    }
}

