﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfYearUI : MonoBehaviour {

    private GameManager gameManager;

    //Text Zones
    public Text yearlyObjectives;
    public Text nectarGoal;
    public Text resinGoal;
    public Text waterGoal;
    public Text pollenGoal;
    public Text workersCreated;
    public Text workersDead;
    public Button nextYear;
    public Button returnMainMenu;

    [Header("Main Menu Scene")]
    public string menuScene;


    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void EndOfYear(){

        nectarGoal.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
        resinGoal.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
        waterGoal.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
        pollenGoal.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);

        if (gameManager.VerifyAllObjectives())
        {
            yearlyObjectives.text = "You have succeeded. Your hive will survive winter.";
            yearlyObjectives.color = Color.green;
            nextYear.gameObject.SetActive(true);
            returnMainMenu.gameObject.SetActive(false);
        }
        else
        {
            yearlyObjectives.text = "You have failed. Your hive did not last winter.";
            yearlyObjectives.color = Color.red;
            nextYear.gameObject.SetActive(false);
            returnMainMenu.gameObject.SetActive(true);
        }
    }

    public void NextYear()
    {
        gameManager.AdvanceYear();
        this.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(menuScene);
    }
}
