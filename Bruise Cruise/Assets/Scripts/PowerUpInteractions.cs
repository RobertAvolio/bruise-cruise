using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractions : MonoBehaviour
{
    public GameObject prefab;

    private bool powered;
    // Start is called before the first frame update
    void Start()
    {
        powered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 3")) && powered)
        {
            Instantiate(prefab, new Vector3(this.gameObject.transform.position.x+10, this.gameObject.transform.position.y + 2, 0), Quaternion.identity);
        }
    }

    public void StartPower()
    {

        powered = true;
        
    }
}
