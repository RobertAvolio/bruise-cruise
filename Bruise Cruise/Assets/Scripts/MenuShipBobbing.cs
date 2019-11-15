using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipBobbing : MonoBehaviour
{
    float originalX, originalY, originalZ;
    public float bobbingMultiplier = .5f;
    public float rollingMultiplier = .2f;
    private Quaternion startRotation;

    void Start()
    {
        originalY = this.transform.position.y;
        originalX = this.transform.position.x;
        originalZ = this.transform.position.z;
        startRotation = this.transform.rotation;
    }

    void Update()
    {
        BobUpAndDown();
        RollSideToSide();
    }

    void BobUpAndDown()
    {
        transform.position = new Vector3(transform.position.x, originalY + 
                                         ((float) Mathf.Sin(Time.time + bobbingMultiplier) * bobbingMultiplier),
                                         transform.position.z);
    }

    void RollSideToSide()
    {
        //transform.position = new Vector3(originalX + (float)Mathf.Sin(Time.time + rollingMultiplier) 
        //                                * rollingMultiplier, transform.position.y, transform.position.z);
        float f = Mathf.Sin(Time.time * rollingMultiplier) * 3f;
        transform.rotation = startRotation * Quaternion.AngleAxis(f, Vector3.up); ;
    }

}
