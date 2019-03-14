using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    public GameObject[] toHide;

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
}
