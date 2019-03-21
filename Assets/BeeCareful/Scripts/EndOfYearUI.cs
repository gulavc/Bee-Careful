using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfYearUI : MonoBehaviour {

    private GameManager gameManager;

    //Text Zones
    public Text pollenGoal;
    public Text resinGoal;
    public Text nectarGoal;
    public Text waterGoal;
    public Text workersCreated;
    public Text workersDead;


    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void EndOfYear(){
        if (gameManager.VerifyAllObjectives())
        {
            pollenGoal.text = "U win";
        }
        else
        {
            pollenGoal.text = "U lose";
        }
    }

    public void NextYear()
    {
        gameManager.AdvanceYear();
        this.gameObject.SetActive(false);
    }

}
