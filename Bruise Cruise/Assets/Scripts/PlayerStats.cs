using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private HealthBar healthbar;
    public float alcoholContent;
    public float timer;

    [SerializeField]
    private SpriteMaskScroll sms;


    private static int _playerHealth = 100;
    private void Awake()
    {
        alcoholContent = 0f;
    }
    private void Update()
    {
        sms.ChangeDrunkness(alcoholContent);
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if (alcoholContent > 0)
        {
            alcoholContent -= Time.deltaTime;
        }
    }



    public void TakeDamage()
    {
        _playerHealth -= 10;
        healthbar.DecreaseHealthBar(10);
    }

    public static int PlayerHealth {
        get => _playerHealth;
    }
}
