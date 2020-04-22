using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
  This class essentially allows the AI to attack the target when in radius.
*/

public class Enemy_Attack : MonoBehaviour
{
    //Declarations
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    public float distance;
    private Enemy_Movement obj;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
      // Debug.Log("Welcome to Bruise-Cruise");
      // obj = GetComponent<Enemy_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
      //Does nothing now
    }
    void AttackPlayer()
    {
      distance = obj.getDistance();

      // Found the target and is some radius away from it
      if(distance <= obj.stoppingDistance)
      {
        isAttacking = true;
        //Debug.Log("target found");
        //AI is next to player and can now start the timer
        timer += Time.deltaTime;

        //AI may start attacking the player when 2 seconds has passed
        if(timer >= waitTime)
        {
          //Debug.Log("timer equals wait time");
          FakeAttack();
          Attack();
          timer = 0.0f;
        }
        isAttacking = false;
      }
    }
    // Function to display attack message to the console
    void FakeAttack()
    {
      //Debug.Log("Attacking the Player!");
    }

    //Function to attack the player (damage)
    void Attack()
    {
      //Debug.Log("Attacking in progress..");
    }
}
