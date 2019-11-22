using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject confirmButtons;
    public GameObject mainButtons;
    private static bool mainActive = false;
    private static bool confirmActive = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if (mainActive)
        {
            pauseMenu.SetActive(false);
            mainActive = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            mainActive = true;
        }
    }

    public void confirmExit()
    {
        if (confirmActive)
        {
            mainButtons.SetActive(true);
            confirmButtons.SetActive(false);
            confirmActive = false;
        }
        else
        {
            mainButtons.SetActive(false);
            confirmButtons.SetActive(true);
            confirmActive = true;
        }
    }
    public void loadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }


}
