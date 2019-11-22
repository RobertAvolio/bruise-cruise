using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractions : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            //Use();  much more complicated. Need to directly addess the CURRENT powerup selected in the selector wheel. Need to use ItemSystem maybe? Maybe a redesign?
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pickup")    
        {
            PowerUp pu = collision.gameObject.GetComponent<PowerUp>();
            pu.StartPowerUp();
        }   
    }
}
