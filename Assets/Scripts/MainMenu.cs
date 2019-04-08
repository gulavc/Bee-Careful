using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject playButton, editButton, loadingText;
    public float rotationSpeed;

    public void PlayGame(int loadMode)
    {
        GameLoader.LoadMode = (LoadMode)loadMode; 
        SceneManager.LoadSceneAsync("TestWorld");

        playButton.SetActive(false);
        editButton.SetActive(false);

        loadingText.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
