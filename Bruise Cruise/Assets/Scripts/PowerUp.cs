using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected bool on = false;
    protected int uses;
    [SerializeField]
    protected int maxUses;

    private void Awake()
    {
        uses = maxUses;
    }

    public void StartPowerUp()
    {
        on = true;

        //disable mesh renderer, collider 
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Use()
    {
        uses -= 1;
        if(uses <= 0)
        {
            on = false;
        }
    }
}

