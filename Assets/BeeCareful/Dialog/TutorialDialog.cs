using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour {

    public Text panelText;
    public Text buttonText;

    public string[] tutorialText;
    int currentText;

	// Use this for initialization
	void Start () {
        ShowText();

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

        panelText.text = tutorialText[currentText];
    }
	
    public void ContinueText()
    {
        if(currentText + 1 >= tutorialText.Length)
        {
            buttonText.text = "Done";
        }
        panelText.text = tutorialText[currentText];
    }

    public void ClickButton()
    {
        currentText++;
        if (currentText >= tutorialText.Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            ContinueText();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
