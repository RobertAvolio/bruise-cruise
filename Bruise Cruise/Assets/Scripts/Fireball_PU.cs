using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_PU : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PowerUpInteractions>().StartPower();
            this.gameObject.SetActive(false);
        }
    }
}
