using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShiftTrigger : MonoBehaviour
{
    [SerializeField] private LevelChange lvl;

    [SerializeField] private bool isUp = false;
    [SerializeField] private bool isRight = false;  
    [SerializeField] private bool isDown = false;

    private bool wasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (wasActivated == false)
            {
                // Could do a switch/case thing that might look prettier, same result
                if (isUp) lvl.ShiftCameraUp();
                if (isRight) lvl.ShiftCameraRight();
                if (isDown) lvl.ShiftCameraDown();
                
                wasActivated = true;
            }
            
        }
    }
}
