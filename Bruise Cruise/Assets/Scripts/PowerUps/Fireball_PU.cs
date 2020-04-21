using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_PU : PowerUp
{
    public GameObject fireballPrefab;
    public GameObject player;
    private float attackTimer;

    private float spawnDistanceX = 2;
    private float spawnDistanceY = 5;

    [SerializeField]
    private float timeBetweenAttack = 2;
    void Update()
    {
        if (!sick)
        {
            if (attackTimer <= 0 && (on && (Input.GetKeyDown("x") || Input.GetKeyDown("joystick button 3"))))
            {
                Vector3 spawnPos = new Vector3(player.transform.position.x + spawnDistanceX, player.transform.position.y + spawnDistanceY);
                Instantiate(fireballPrefab, spawnPos, player.transform.rotation);
                attackTimer = timeBetweenAttack;
                Use();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }

        sick = movement.cannot_move;
    }
}
