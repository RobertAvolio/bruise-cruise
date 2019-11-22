using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_PU : PowerUp
{
    public GameObject fireballPrefab;
    public GameObject player;
    private float timer;

    private float spawnDistanceX = 2;
    private float spawnDistanceY = 5;

    [SerializeField]
    private float timeBetweenAttack = 2;
    void Update()
    {
        if (timer <= 0 && (on && Input.GetKeyDown("x") || Input.GetKeyDown("joystick button 3")))
        {
            Vector3 spawnPos = new Vector3(player.transform.position.x + spawnDistanceX, player.transform.position.y + spawnDistanceY);
            Instantiate(fireballPrefab, spawnPos, player.transform.rotation);
            Use();
            timer = timeBetweenAttack;
        } else
        {
            timer -= Time.deltaTime;
        }
    }
}
