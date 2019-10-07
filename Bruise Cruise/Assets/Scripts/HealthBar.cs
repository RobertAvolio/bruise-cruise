using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public int health = 100;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        if (Input.GetKeyDown(KeyCode.O)) //Replace input as needed. Duplicate this if multiple damage types exist
        {
            if (health > 0)
            {
                health-=10; //change int according to amount of dmg supposed to be done
                if(health<0)
                {
                    health = 0;
                }
                healthBar.value = (float)health / 100;

            }
        }
        if (Input.GetKeyDown(KeyCode.P)) //Replace input as needed. Duplicate this is multiple healing types exist
        {
            if (health < 100)
            {
                health+=15; //change int according to amount of healing supposed to be done
                if(health>100)
                {
                    health = 100;
                }
                healthBar.value = (float)health / 100;
            }
        }
    }
}
