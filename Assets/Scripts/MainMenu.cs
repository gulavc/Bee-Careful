using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject playButton, editButton, loadingText;

    public void PlayGame(int loadMode)
    {
        GameLoader.LoadMode = (LoadMode)loadMode; 
        SceneManager.LoadSceneAsync("TestWorld");

        playButton.SetActive(false);
        editButton.SetActive(false);

        loadingText.SetActive(true);
    }
}
