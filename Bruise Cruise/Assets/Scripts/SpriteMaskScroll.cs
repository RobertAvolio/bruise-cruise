using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskScroll : MonoBehaviour
{
    private int drunkLevel = 0;
    private int startPos;
    public GameObject drunkMeter;
    private int endPos;
    private SpriteRenderer bottleSprite;
    public Gradient gradient;
    void Start()
    {
        bottleSprite = drunkMeter.GetComponent<SpriteRenderer>();
        bottleSprite.color = gradient.Evaluate(0f);
        startPos = (int)transform.localPosition.y;
        endPos = startPos + 95; //Range of sprite mask slider
    }

    void ResetDrunkMeter() //Resets drunkness to zero
    {
        transform.localPosition = new Vector3(transform.localPosition.x, startPos, transform.localPosition.z);
        drunkLevel = 0;
    }

    public void ChangeDrunkness(float value) //Send in value(neg or pos) by how much you want to change drunkness meter
    {
        drunkLevel = (int)value;
        transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
        //StartCoroutine(Transition(transform.localPosition.y, value));
    }

    //Will cause drunkmeter to change colors and slide smoothly when drunk level is changed.
    IEnumerator Transition(float startValue, int changeValue)
    {
        if(changeValue<0)//if drunkness is decreasing
        {
            if (startValue + changeValue < startPos) //if drunkness exceeds past 0
            {
                for(float ft = startValue; ft>=startPos; ft-=1f) //Changes drunkness level and color of meter incrementally
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, ft, transform.localPosition.z);
                    drunkLevel -= 1;
                    bottleSprite.color = gradient.Evaluate(drunkLevel / 100f);
                    yield return null;
                }
            }
            else
            {
                for (float ft = startValue; ft >= startValue+changeValue; ft -= 1f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, ft, transform.localPosition.z);
                    drunkLevel -= 1;
                    bottleSprite.color = gradient.Evaluate(drunkLevel / 100f);
                    yield return null;
                }
            }
        }
        else //if drunkness is increasing
        {
            if (startValue + changeValue > endPos) //if drunkness exceeds past 100
            {
                for (float ft = startValue; ft <= endPos; ft += 1f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, ft, transform.localPosition.z);
                    drunkLevel += 1;
                    bottleSprite.color = gradient.Evaluate(drunkLevel / 100f);
                    yield return null;
                }
            }
            else
            {
                for (float ft = startValue; ft <= startValue + changeValue; ft += 1f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, ft, transform.localPosition.z);
                    drunkLevel += 1;
                    bottleSprite.color = gradient.Evaluate(drunkLevel / 100f);
                    yield return null;
                }
            }
        }
    }
}
