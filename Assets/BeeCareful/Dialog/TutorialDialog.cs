using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour {

    private const float textSpeed = 0.03f;

    public Text panelText;
    public Text buttonText;
    private Button button;

    public bool allowCompletion = true;

    public string[] tutorialText;
    int currentText;

	// Use this for initialization
	void Start () {
        ShowText();
        button = GetComponentInChildren<Button>();
    }

    public void ShowText()
    {
        currentText = 0;
        if(tutorialText.Length == 1)
        {
            buttonText.text = "Done";
        }
        else
        {
            buttonText.text = "Continue";
        }

        StartCoroutine(ShowText(tutorialText[currentText]));
    }
	
    public void ContinueText()
    {
        StopAllCoroutines();
        if(currentText + 1 >= tutorialText.Length)
        {
            if (allowCompletion)
            {
                buttonText.text = "Done";
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
        StartCoroutine(ShowText(tutorialText[currentText]));
    }

    public void ClickButton()
    {
        currentText++;
        if (currentText >= tutorialText.Length)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            ContinueText();
        }
    }


    IEnumerator ShowText(string text)
    {
        char[] arrayText = text.ToCharArray();
        panelText.text = "";

        foreach(char c in arrayText)
        {
            panelText.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
