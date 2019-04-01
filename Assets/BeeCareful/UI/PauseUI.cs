using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    public GameObject[] toHide;
    public GameObject optionsUI;
    public string menuScene;
	
    public void ReturnToGame()
    {
        Hide();
    }

    public void MainMenu()
    {
        foreach(GameObject g in toHide)
        {
            g.SetActive(false);
        }

        SceneManager.LoadSceneAsync(menuScene);
    }

    public void ShowOptions()
    {
        optionsUI.SetActive(true);
    }

    public void HideOptions()
    {
        optionsUI.SetActive(false);
    }        

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    //Options setters & getters
    public void SetShowHivePins(bool value)
    {
        Options.ShowHivePins = value;
    }

    public void SetShowMountainPins(bool value)
    {
        Options.ShowMountainPins = value;
    }

    public void SetShowBeePins(bool value)
    {
        Options.ShowBeePins = value;
    }

    public void SetShowResourcePins(bool value)
    {
        Options.ShowResourcePins = value;
    }

    public void SetShowDangerPins(bool value)
    {
        Options.ShowDangerPins = value;
    }

    public void SetShowGrid(bool value)
    {
        Options.ShowGrid = value;
    }
}
