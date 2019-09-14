using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTimer : MonoBehaviour
{
    public Attack attack1;
    public Attack attack2;
    public Attack attack3;
    private Renderer rend1;
    private Renderer rend2;
    private Renderer rend3;
    private BoxCollider2D box1;
    private BoxCollider2D box2;
    private BoxCollider2D box3;
    private float timer;
    private bool hit2;
    // Start is called before the first frame update
    void Start()
    {
        rend1 = attack1.GetComponent<Renderer>();
        rend2 = attack2.GetComponent<Renderer>();
        rend3 = attack3.GetComponent<Renderer>();
        box1 = attack1.GetComponent<BoxCollider2D>();
        box2 = attack2.GetComponent<BoxCollider2D>();
        box3 = attack3.GetComponent<BoxCollider2D>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        rend1.enabled = false;
        box1.enabled = false;
        rend2.enabled = false;
        box2.enabled = false;
        rend3.enabled = false;
        box3.enabled = false;
        if (Input.GetKeyUp("e"))
        {
            if(timer <= 0)
            {
                rend1.enabled = true;
                box1.enabled = true;
                timer = 3;
            } else if(!hit2)
            {
                rend2.enabled = true;
                box2.enabled = true;
                hit2 = true;
            }
            else
            {
                rend3.enabled = true;
                box3.enabled = true;
                timer = 0;
            }
            
            
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            hit2 = false;
        }

    }
}
