using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_PU : PowerUp
{
    public GameObject fireballPrefab;
    public GameObject player;
    void Update()
    {
        if ((on && Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 3")))
        {
            Instantiate(fireballPrefab, new Vector3(player.transform.position.x + 10, player.transform.position.y + 2, 0), Quaternion.identity);
            Use();
        }
    }


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PowerUpInteractions>().StartPower();
            this.gameObject.SetActive(false);
        }
    }
    */
}
