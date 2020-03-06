using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tequila_PU : PowerUp
{
    public GameObject limePrefab;
    public GameObject player;
    public GameObject controller;
    private float spawnDistanceX = 8f;
    private float spawnDistanceY = 0f;
    private float attackTimer;
    [SerializeField]
    private float timeBetweenAttack = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer <= 0 && (on && (Input.GetKeyDown("x") || Input.GetKeyDown("joystick button 3"))))
        {
            if (player.transform.eulerAngles.y >= 180 && spawnDistanceX > 0)
            {
                spawnDistanceX *= -1;
            } else if(player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y < 180 && spawnDistanceX < 0)
            {
                spawnDistanceX *= -1;
            }
            Vector3 spawnPos = new Vector3(player.transform.position.x + spawnDistanceX, player.transform.position.y + spawnDistanceY, player.transform.position.z);
            controller.transform.SetPositionAndRotation(spawnPos, player.transform.rotation);
            GameObject go = Instantiate(limePrefab, new Vector3(0, 0, 0), player.transform.rotation) as GameObject;
            go.transform.parent = controller.transform;
            
            Use();

            attackTimer = timeBetweenAttack; // i have no idea why timebetweenattack doesn't do anything when changed. 1000 is the same as 0.
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
}
