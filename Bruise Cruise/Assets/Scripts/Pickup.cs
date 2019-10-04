using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public PowerUp pu;  

    public void PowerUpOn()
    {
        pu.StartPowerUp();
    }

    public void PowerOff()
    {
        this.gameObject.SetActive(false);
    }
}
