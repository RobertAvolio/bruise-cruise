using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractions : MonoBehaviour
{
    private const int SIZE = 4;
    private int currentPowerIndex;
    [SerializeField]
    PowerUp[] powerUps;
    int numPowers;
    private void Start()
    {
        powerUps = new PowerUp[4];
        numPowers = 0;
        currentPowerIndex = -1;
    }
    private void Update()
    {
        if(Input.GetKeyDown("x") && currentPowerIndex != -1) // checking if the index doesn't equal -1 to check if one has been added or not
        {
            powerUps[currentPowerIndex].Use();
            //Use();  much more complicated. Need to directly addess the CURRENT powerup selected in the selector wheel. Need to use ItemSystem maybe? Maybe a redesign?
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pickup")  // might need to add a max check to make sure they don't add a 5th? Already happens in AddPowerUp, though.  
        {
            PowerUp pu = collision.gameObject.GetComponent<PowerUp>();
            AddPowerUp(pu);
            pu.StartPowerUp();
        }   
    }

    public void AddPowerUp(PowerUp p)
    {
        if (numPowers != SIZE)
        {
            powerUps[numPowers] = p;
            numPowers++;
            currentPowerIndex = numPowers - 1;
        }
        else
        {
            Debug.Log("Power up array is full.");
        }

    }
}
