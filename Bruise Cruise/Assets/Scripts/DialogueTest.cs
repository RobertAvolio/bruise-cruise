using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : ScriptableObject
{
    public GameObject DialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.GetComponent<Dialogue>().SetDialogueTextFilePath("Assets/Scenes/DialogueFiles/test1.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DialogueBox.SetActive(true);
        }
    }
}
