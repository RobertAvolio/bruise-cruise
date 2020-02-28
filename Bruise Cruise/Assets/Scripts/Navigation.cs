using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public List<Transform> levels;
    private int currentPosition;
    private bool isTransitioning;
    private float timer;
    private const int START = 0;
    private const float TIMER_LENGTH = 0.2f;
    
    private void Awake()
    {
        // Start at the position of the first level by default
        currentPosition = 0;
        isTransitioning = false;
        timer = 0;
    }
    
    private void Update()
    {
        //isTransitioning = false;
        timer -= Time.deltaTime;
        var moveScreen = Input.GetAxisRaw("Horizontal");

        // Moving down the level list
        if (moveScreen >= 1 && timer <= 0)
        {
            // Won't go past the end of the list
            if (currentPosition + 1 != levels.Count)
            {
                currentPosition++;
                ChangeLevel(currentPosition);
                timer = TIMER_LENGTH;
            }
            // Goes past, wrap around to begin of list
            else
            {
                currentPosition = START;
                ChangeLevel(currentPosition);
                timer = TIMER_LENGTH;
            }
            
        }

        // Moving up the level list
        if (moveScreen <= -1 && timer <= 0)
        {
            // Won't go past the beginning of the list
            if (currentPosition - 1 != START - 1)
            {
                currentPosition--;
                ChangeLevel(currentPosition);
                timer = TIMER_LENGTH;
            }
            // Goes past, wrap around to end of list
            else
            {
                currentPosition = levels.Count - 1;
                ChangeLevel(currentPosition);
                timer = TIMER_LENGTH;
            }
        }
    }

    private void ChangeLevel(int next)
    {
        print($"Current position: {transform.position}");
        print($"Next position: {levels[next].position}");
        transform.position = levels[next].position;
        transform.LookAt(levels[next]);
        
    }
}
