using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TequilaInteractions : MonoBehaviour
{
    private List<GameObject> gameObjectsOut;
    private List<GameObject> gameObjectsReturn;
    private bool isReturning = false;
    private int count;
    private float spawnPos;
    private bool firstFrame = true;
    private GameObject chad;
    private float rotation;

    private void Start()
    {
        count = 0;
        chad = GameObject.Find("Chad");
        rotation = chad.transform.eulerAngles.y;
    }
    
    private void Update()
    {
        if(firstFrame)
        {
            spawnPos = this.gameObject.transform.position.x;
            firstFrame = false;
        }

        if(rotation == 0 && this.gameObject.transform.position.x <= spawnPos)
        {
            count++;
            if(count == 2)
            {
                this.gameObject.SetActive(false);
            }
        } else if(rotation == 180 && this.gameObject.transform.position.x >= spawnPos)
        {
            count++;
            if (count == 2)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // i took out the list because it wasn't working :[
        if (col.gameObject.tag == "Enemy") //&& !gameObjectsOut.Contains(col.gameObject)
        {
            col.GetComponent<Enemy_AI_Duel_StateMachine>().TakeDamage();
            //gameObjectsOut.Add(col.gameObject);
        }
        else if (col.gameObject.tag == "Enemy")// && !gameObjectsReturn.Contains(col.gameObject)
        {
            col.GetComponent<Enemy_AI_Duel_StateMachine>().TakeDamage();
            //gameObjectsReturn.Add(col.gameObject);
        }

        if(col.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}