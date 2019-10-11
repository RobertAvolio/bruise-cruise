/*
  Project: 2D Action Platformer (Shovel Knight-esque)
  Description: AI state machine that controls each state and making sure that you are unable to perform two sub-states at once.
  Commit: Leveraged switch statements to allow a smooth maneuver from one state machine to another.
  Date: 10/10/2019
*/

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_STATE{patrol, chase, attack}       // Enumeration for each state machine

public class AI_State_Machine : MonoBehaviour
{
  // Declarations
  private float waitTime = 2.0f;                  // Declare and set wait time
  private float timer = 0.0f;                     // Declare and set timer

  public float speed;                             // Declare the speed

  public float distance;                          // Declare distance from player
  public float attackingRadius;                   // Declare radius to attack the player within certain radius
  public float lookingRadius;                     // Declare radius to look for the player within certain radius

  private AI_STATE currState = AI_STATE.patrol;   // Declare and initialize the current state machine

  private Transform player;

  /* start is called before the first frame update */
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
  }

  /* update is called once per frame */
  void Update()
  {
    UpdateDistance();                           // Update distance between enemy and player

    switch(currState)                           // Switch statement to maneuver between states
    {
      case AI_STATE.patrol:                     // Patroling state
        Patroling();
        break;
      case AI_STATE.chase:                      // Chasing state
        ChasingPlayer();
        break;
      case AI_STATE.attack:                     // Attacking state
        AttackingPlayer();
        break;
    }
  }

  /* Function to look out for player */
  void Patroling()
  {
    if(WithinLookingRadius())                  // Enemey can see the player then move to chasing state
    {
      currState = AI_STATE.chase;
    }
  }

  /* Function to chase after the player */
  void ChasingPlayer()
  {
    // Chase after the player
    if(!WithinAttackingRadius() && WithinLookingRadius())        // Enemy can see the player but not within attacking radius, then keep chasing player until enemy is within attacking radius
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);        // Update targets position
    }
    else if(!WithinLookingRadius())                              // Enemy is not able to see the player, cannot chase after anymore
    {
      currState = AI_STATE.patrol;
    }
    else                                                         // Enemy can see player and is within attacking radius
    {
      currState = AI_STATE.attack;
    }
  }

  /* Function to attack the player */
  void AttackingPlayer()
  {
    if(WithinAttackingRadius())                                // Attack the player if enemy is within the attacking radius
    {
      timer+=Time.deltaTime;                                   // Update timer

      if(timer >= waitTime)                                    // Only attack the player when enemy is within the attacking radius
      {
        DisplayAttackMessage();
        DealDamage();
        timer = 0.0f;
      }
    }
    else
    {
      currState = AI_STATE.chase;
    }
  }

  /* Function to display attack message to the console */
  void DisplayAttackMessage()
  {
    Debug.Log("Attacking the Player!");
  }

  /* Function to deal damage */
  void DealDamage()
  {
    Debug.Log("Damage in progess..");
  }

  /*  Function to update the distance between enemy and player */
  void UpdateDistance()
  {
    distance = Mathf.Abs(Vector3.Distance(transform.position, player.position));
  }
  /* Function to determine if player is within attacking radius */
  bool WithinAttackingRadius()
  {
    return (distance <= attackingRadius);
  }

  /* Function to determine if player is within looking radius */
  bool WithinLookingRadius()
  {
    return (distance <= lookingRadius);
  }
}
