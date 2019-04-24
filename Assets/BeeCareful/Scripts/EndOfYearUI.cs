using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfYearUI : MonoBehaviour {

    private GameManager gameManager;

    [Header("Text Zones")]
    public Text yearlyObjectives;
    public Text nectarGoal;
    public Text resinGoal;
    public Text waterGoal;
    public Text pollenGoal;
    public Button nextYear;
    public Button returnMainMenu;

    [Header("Main Menu Scene")]
    public string menuScene;

    [Header("Sounds")]
    public AudioClip objectiveWin;
    public AudioClip objectiveLose;
    public AudioClip endOfYearMusic;


    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void EndOfYear(){

        gameManager.PlayMusic(endOfYearMusic);

        nectarGoal.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
        resinGoal.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
        waterGoal.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
        pollenGoal.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);

        if (gameManager.VerifyAllObjectives())
        {
            yearlyObjectives.text = "Complete. Your hive will survive winter.";
            yearlyObjectives.color = Color.green;
            nextYear.gameObject.SetActive(true);
            returnMainMenu.gameObject.SetActive(false);

            gameManager.PlaySFX(objectiveWin);
        }
        else
        {
            yearlyObjectives.text = "Your hive did not last winter. You have failed.";
            yearlyObjectives.color = Color.red;
            nextYear.gameObject.SetActive(false);
            returnMainMenu.gameObject.SetActive(true);

            gameManager.PlaySFX(objectiveLose);
        }
    }

    public void NextYear()
    {
        gameManager.StopMusic();
        gameManager.AdvanceYear();
        this.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(menuScene);
    }
}
