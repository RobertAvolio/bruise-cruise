using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private int lineCount = 1;
    public GameObject leftPhotoBox;
    public GameObject rightPhotoBox;
    public GameObject rImage;
    public GameObject lImage;
    private GameObject currentBox;
    private bool boxActive = false;
    public DialoguePhotoSignal photo;
    public string textFilePath;
    private StreamReader reader;
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    private int numCharIgnore = 2;
    public TextMeshProUGUI dialogueText;

    void Start()
    {
        reader = new StreamReader(textFilePath);
        currentBox = rightPhotoBox;
        fullText = reader.ReadLine();
        photo.changePhoto(fullText.Substring(0, numCharIgnore - 1));
        if(currentBox == rightPhotoBox)
        {
            rImage.GetComponent<Image>().sprite = photo.GetCurrentPhoto();
        }
        if (currentBox == leftPhotoBox)
        {
            lImage.GetComponent<Image>().sprite = photo.GetCurrentPhoto();
        }
        fullText = fullText.Substring(numCharIgnore);
        StartCoroutine(ShowText());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            switch (boxActive)
            {
                case true: leftPhotoBox.SetActive(false); rightPhotoBox.SetActive(true); boxActive = false; currentBox = rightPhotoBox;  break;
                case false: rightPhotoBox.SetActive(false); leftPhotoBox.SetActive(true); boxActive = true; currentBox = leftPhotoBox;  break;
            }
            
            fullText = reader.ReadLine();
            if (fullText != "END")
            {
                photo.changePhoto(fullText.Substring(0, numCharIgnore - 1));
                if (currentBox == rightPhotoBox)
                {
                    rImage.GetComponent<Image>().sprite = photo.GetCurrentPhoto();
                    dialogueText.transform.localPosition = new Vector2(-50, 0);
                }
                if (currentBox == leftPhotoBox)
                {
                    lImage.GetComponent<Image>().sprite = photo.GetCurrentPhoto();
                    dialogueText.transform.localPosition = new Vector2(100, 0);
                }
                fullText = fullText.Substring(numCharIgnore);
                
                StartCoroutine(ShowText());
                
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SetDialogueTextFilePath(string path)
    {
        textFilePath = path;
        reader = new StreamReader(textFilePath);
    }

    IEnumerator ShowText()
    {
        for(int i=0; i<fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
