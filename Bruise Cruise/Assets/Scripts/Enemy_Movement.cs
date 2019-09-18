using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float distance;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
      target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    // Method for enemy to move from his position to targets position
    void Update()
    {
      //Keep moving towards the target until the distance <= 3
      distance = Mathf.Abs(Vector2.Distance(transform.position, target.position));
      if(distance > stoppingDistance)
      {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }
    }
}
