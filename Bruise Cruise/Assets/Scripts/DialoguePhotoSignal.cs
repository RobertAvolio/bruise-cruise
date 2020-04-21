using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialoguePhotoSignal : ScriptableObject
{
    private Sprite currentPhoto;

    //Add sprite images into the DATABASE
    public Sprite Eustice;
    public Sprite Chad;

    public Sprite GetCurrentPhoto()
    {
        return currentPhoto;
    }
    public void changePhoto(string initial)
    {
        switch(initial)
        {
            case "C": currentPhoto = Chad; break;
            case "E": currentPhoto = Eustice; break;
        }
    }
}
