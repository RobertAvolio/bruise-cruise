using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
  This class essentially allows the AI to obtain the position of the target and
  move towards it.
*/
public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private float distance;
    private Transform target;
    private bool isMovingTowardsPlayer = false; 

    // Start is called before the first frame update
    void Start()
    {
      // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    // Method for enemy to move from his position to targets position
    void Update()
    {
      //Does Nothing
    }
    void MoveTowardsPlayer()
    {
      //Keep moving towards the target until the distance <= 3
      distance = Mathf.Abs(Vector2.Distance(transform.position, target.position));
      if(distance > stoppingDistance)
      {
        isMovingTowardsPlayer = true;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }
      isMovingTowardsPlayer = false;
    }
    public float getDistance()
    {
      return distance;
    }
}
