// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public static class PlayerStats
{
    private static int playerHealth = 100;

    public static int GetHealth()
    {
        return playerHealth;
    }

    public static void SetHealth(int health)
    {
        playerHealth = health;
    }
}
