using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    public GameObject[] positions;
    public static bool isLevelShifting = false;
    [SerializeField] private Camera cam;
    private Vector3 targetPosition;
    private float speed = 1000f;
    private int positionIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (isLevelShifting == true) {
            float step = speed * Time.deltaTime;

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPosition, step);
            
            if (Vector3.Distance(cam.transform.position,targetPosition) < 0.001f) {
                positionIndex++;
                isLevelShifting = false;
            }
        }
    }

    //
    // These functions will move the camera over so we don't call it every frame
    //
    
    public void ShiftCameraRight() {
        
        // Set our target position for the camera shift to the next area
        if(positionIndex >= positions.Length) return;
 
        targetPosition = new Vector3(positions[positionIndex].transform.position.x, cam.transform.position.y, cam.transform.position.z);
        
        // Start the process for shifting the scene
        isLevelShifting = true;
    }

    public void ShiftCameraUp() {

        // Set our target position for the camera shift to the next area
        if (positionIndex >= positions.Length) return;
        targetPosition = new Vector3(cam.transform.position.x, positions[positionIndex].transform.position.y, cam.transform.position.z);
        
        // Start the process for shifting the scene
        isLevelShifting = true;
    }

    public void ShiftCameraDown() {

        // Set our target position for the camera shift to the next area 
        if (positionIndex >= positions.Length) return;
        targetPosition = new Vector3(cam.transform.position.x, positions[positionIndex].transform.position.y, cam.transform.position.z);
        
        // Start the process for shifting the scene
        isLevelShifting = true;
    }
}
