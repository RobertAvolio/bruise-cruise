using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineStuff;

public class FIsh_AI : MonoBehaviour
{
    public enum actions
    {
        pos1,
        pos2,
        attack
    }
    [SerializeField]
    public Vector3 position1, position2;
    public List<actions> actionList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
