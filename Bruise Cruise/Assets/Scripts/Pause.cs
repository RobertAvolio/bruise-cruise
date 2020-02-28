using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pauseGUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseGUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseGUI.activeInHierarchy)
            {
                //pause
                Time.timeScale = 0;
                pauseGUI.SetActive(true);
            } else if (pauseGUI.activeInHierarchy)
            {
                //continue
                Time.timeScale = 1;
                pauseGUI.SetActive(false);
            }
        }
    }
}
