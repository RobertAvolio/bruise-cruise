using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + PlayerStats.GetHealth();
        healthBar.value = (float)PlayerStats.GetHealth() / 100;
    }

    void healthDecrease(int numDecrease)
    {
        int health = PlayerStats.GetHealth();
        if (health > 0)
        {
            health -= numDecrease;
            if (health < 0)
            {
                health = 0;
            }
            PlayerStats.SetHealth(health);
        }
    }

    void healthIncrease(int numIncrease)
    {
        int health = PlayerStats.GetHealth();
        if (health < 100)
        {
            health += numIncrease;
            if (health > 100)
            {
                health = 100;
            }
            PlayerStats.SetHealth(health);
        }
    }
}
