using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShiftTrigger : MonoBehaviour
{
    [SerializeField] private LevelChange lvl;

    [SerializeField] private bool isRight = false;  
    [SerializeField] private bool isPitfall = false;

    private bool wasActivated = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (wasActivated == false)
            {

                if (isRight)
                {
                    this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                }
                //lvl.ShiftCamera();

                wasActivated = true;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isPitfall)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
