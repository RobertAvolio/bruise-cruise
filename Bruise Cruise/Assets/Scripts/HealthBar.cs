﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (health <= 0)
        {
            var scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }

    public void IncreaseHealthBar(int value)
    {
        if (health < 100)
        {
            health += value; 
            if (health > 100)
            {
                health = 100;
            }
            healthBar.value = (float)health / 100;
        }
    }

    public void DecreaseHealthBar(int value)
    {
        if (health > 0)
        {
            health -= value;
            if (health < 0)
            {
                health = 0;
            }
            healthBar.value = (float)health / 100;

        }
    }
}
