// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class PlayerStats
{
    private static int _playerHealth = 100;

    public void TakeDamage()
    {
        _playerHealth -= 10;
    }

    public static int PlayerHealth {
        get => _playerHealth;
    }
}
