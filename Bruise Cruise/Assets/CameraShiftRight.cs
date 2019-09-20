using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftRight : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            cam.transform.Translate(Vector3.MoveTowards(cam.transform.position, new Vector3(cam.transform.position.x + 512, cam.transform.position.y, cam.transform.position.z), 512));
          //  move the player a bit too
        }
    }
}
