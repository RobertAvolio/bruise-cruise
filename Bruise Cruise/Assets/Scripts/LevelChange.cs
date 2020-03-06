using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    //public GameObject[] positions;
    //public static bool isLevelShifting = false;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Vector2 levelDimensions = new Vector2(-1, -1);
    [SerializeField]
    private float transitionSpeed = 20f;
    
    private Vector3 targetPosition;
    private Vector3 cameraOffset = new Vector2(0, 0);
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(levelDimensions.x == -1)
        {
            levelDimensions.x = cam.orthographicSize * cam.aspect * 2;
        }
        if(levelDimensions.y == -1)
        {
            levelDimensions.y = cam.orthographicSize * 2;
        }
        cameraOffset = cam.transform.position - calcTargetPosition();
        targetPosition = calcTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(calcTargetPosition() != targetPosition)
        {
            print("DICK");
        }
        Vector3 newTargetPosition = calcTargetPosition();
        targetPosition = new Vector3(Mathf.Max(newTargetPosition.x, targetPosition.x), newTargetPosition.y, newTargetPosition.z);

        if ((cam.transform.position - (targetPosition + cameraOffset)).magnitude > transitionSpeed * Time.deltaTime)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPosition + cameraOffset, transitionSpeed * Time.deltaTime);
        }
        else
        {
            cam.transform.position = targetPosition + cameraOffset;
        }
    }

    Vector3 calcTargetPosition()
    {
        float targetX = player.position.x - mod(player.position.x - levelDimensions.x / 2, levelDimensions.x);
        float targetY = player.position.y - mod(player.position.y - levelDimensions.y / 2, levelDimensions.y);
        Vector3 newtargetPosition = new Vector3(targetX, targetY, cam.transform.position.z);
        return newtargetPosition;
    }

    //I have to define my own mod because c#'s % is actually the remainder operator so it acts fucky with negative numbers
    float mod(float a, float n)
    {
        float result = a % n;
        if ((result < 0 && n > 0) || (result > 0 && n < 0))
        {
            result += n;
        }
        return result;
    }
}
