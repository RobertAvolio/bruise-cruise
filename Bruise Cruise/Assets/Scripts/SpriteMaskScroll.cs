using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskScroll : MonoBehaviour
{ 
    private int startPos;
    
    private int endPos;
    void Start()
    {
        startPos = (int)transform.localPosition.y;
        endPos = startPos + 95;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.K))
        {
            decreaseDrunk(5);
        }   
        if (Input.GetKeyDown(KeyCode.L))
        {
            increaseDrunk(5);
        }*/
    }

    public void setDrunkness(float alcoholContent)
    {
        transform.localPosition = transform.localPosition + new Vector3(0, alcoholContent, 0);
    }
    /*
    void increaseDrunk(int value)
    {
        if (transform.localPosition.y >= startPos)
        {
            if (transform.localPosition.y + value > endPos)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, endPos, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = transform.localPosition + new Vector3(0, value, 0);
            }
        }
    }

    void decreaseDrunk(int value)
    {
        if (transform.localPosition.y <= endPos)
        {
            if (transform.localPosition.y - value < startPos)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,startPos,transform.localPosition.z);
            }
            else
            {
                transform.localPosition = transform.localPosition - new Vector3(0, value, 0);
            }
        }
    }*/
}
