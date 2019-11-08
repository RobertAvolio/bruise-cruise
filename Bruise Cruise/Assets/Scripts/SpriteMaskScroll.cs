using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskScroll : MonoBehaviour
{ 
    private int startPos;
    
    private int endPos;
    void Start()
    {
        startPos = (int)transform.position.y;
        endPos = startPos + 95;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            decreaseDrunk(5);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            increaseDrunk(5);
        }
    }

    void increaseDrunk(int value)
    {
        if (transform.position.y >= startPos)
        {
            if (transform.position.y + value > endPos)
            {
                transform.position = new Vector3(transform.position.x, endPos, transform.position.z);
            }
            else
            {
                transform.position = transform.position + new Vector3(0, value, 0);
            }
        }
    }

    void decreaseDrunk(int value)
    {
        if (transform.position.y <= endPos)
        {
            if (transform.position.y - value < startPos)
            {
                transform.position = new Vector3(transform.position.x,startPos,transform.position.z);
            }
            else
            {
                transform.position = transform.position - new Vector3(0, value, 0);
            }
        }
    }
}
