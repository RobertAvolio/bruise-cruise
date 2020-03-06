using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private HealthBar healthbar;

    private static int _playerHealth = 100;

    public void TakeDamage()
    {
        _playerHealth -= 10;
        healthbar.DecreaseHealthBar(10);
    }

    public static int PlayerHealth {
        get => _playerHealth;
    }

    
}
