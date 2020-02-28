using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem : MonoBehaviour
{
    public PowerUpInteractions PU;
    public Sprite itemFiller;
    private Sprite[] itemsHeld = new Sprite[4];
    private int itemSelected = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            itemsHeld[i] = itemFiller;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
        //Below change bindings to keyboard/controller for rotating between items

        if (Input.GetKeyDown(KeyCode.K)) //Rotates to next item left, bind input controls as needed
        {
            itemSelected -= 1;
            if (itemSelected < 0)
            {
                itemSelected = 3;
            }
            if (itemsHeld[itemSelected] == itemFiller)
            {
                int slotCount = 0;
                while (slotCount < 3)
                {
                    if (itemsHeld[itemSelected] != itemFiller)
                    {
                        slotCount = 4;
                    }
                    else
                    {
                        itemSelected -= 1;
                        if (itemSelected < 0)
                        {
                            itemSelected = 3;
                        }
                        slotCount += 1;
                    }
                }
            }
            this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
            PU.SetPowerIndex(itemSelected);
        }

        if (Input.GetKeyDown(KeyCode.L)) //Rotates to next item right, bind input controls as needed
        {
            itemSelected += 1;
            if (itemSelected > 3)
            {
                itemSelected = 0;
            }
            if (itemsHeld[itemSelected] == itemFiller)
            {
                int slotCount = 0;
                while (slotCount < 3)
                {
                    if (itemsHeld[itemSelected] != itemFiller)
                    {
                        slotCount = 4;
                    }
                    else
                    {
                        itemSelected += 1;
                        if (itemSelected > 3)
                        {
                            itemSelected = 0;
                        }
                        slotCount += 1;
                    }
                }

            }
            this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
            PU.SetPowerIndex(itemSelected);
        }
    }

    public void addPowerUp(Sprite power)
    {
        int added = 0;
        while (added < 4) //Checks to see all the item slots are filled or not
        {
            if (itemsHeld[itemSelected] == itemFiller)
            {
                itemsHeld[itemSelected] = power;
                added = 4;
            }
            else
            {
                itemSelected += 1;
                if (itemSelected > 3)
                {
                    itemSelected = 0;
                }
                added++; //If all item slots taken up, nothing happens.
            }
        }
        this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
    }


    /*
    //Adds fireball to itemlist (Copy paste this method and replace (fireball) sprite if any more types of items want to be added
    void addFireBall()
    {
        int added = 0;
        while (added < 4) //Checks to see all the item slots are filled or not
        {
            if (itemsHeld[itemSelected] == itemFiller)
            {
                itemsHeld[itemSelected] = fireBall;
                added = 4;
            }
            else
            {
                itemSelected += 1;
                if (itemSelected > 3)
                {
                    itemSelected = 0;
                }
                added++; //If all item slots taken up, nothing happens.
            }
        }
        this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
    }

    void addWine()
    {
        int added = 0;
        while (added < 4) //Checks to see all the item slots are filled or not
        {
            if (itemsHeld[itemSelected] == itemFiller)
            {
                itemsHeld[itemSelected] = wine;
                added = 4;
            }
            else
            {
                itemSelected += 1;
                if (itemSelected > 3)
                {
                    itemSelected = 0;
                }
                added++;
            }
        }
        this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
    }
    
    //Removes current item selected and defaults to your next available item
    public void removeItem()
    {
        itemsHeld[itemSelected] = itemFiller;
        if (itemsHeld[itemSelected] == itemFiller)
        {
            int slotCount = 0;
            while (slotCount < 3) //Looks for available item
            {
                if (itemsHeld[itemSelected] != itemFiller)
                {
                    slotCount = 4;
                }
                else
                {
                    itemSelected -= 1;
                    if (itemSelected < 0)
                    {
                        itemSelected = 3;
                    }
                    slotCount += 1;
                }
            }
        }
        this.gameObject.GetComponent<Image>().sprite = itemsHeld[itemSelected];
    }*/
}
