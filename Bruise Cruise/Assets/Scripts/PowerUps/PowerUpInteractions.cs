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
        if(currentPowerIndex != -1) // checking if the index doesn't equal -1 to check if one has been added or not
        {
            powerUps[currentPowerIndex].on = true;
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
            if(currentPowerIndex != -1)
            {
                powerUps[currentPowerIndex].on = false;
            }
            powerUps[numPowers] = p;
            numPowers++;
            currentPowerIndex = numPowers - 1;

        }
        else
        {
            Debug.Log("Power up array is full.");
        }

    }

    public void SetPowerIndex(int n)
    {
        powerUps[currentPowerIndex].on = false;
        currentPowerIndex = n;
    }

    
}
