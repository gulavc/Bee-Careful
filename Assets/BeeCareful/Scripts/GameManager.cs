using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerResources playerResources;
    public ResourcesHUD resourcesHUD;
    public PointsAction pointsAction;
    public Workers workers;
    public EndOfYearUI endOfYearUI;
    public GlobalObjectives globalObjectives;
    public UpgradeManager upgradeManager;
    public DangerManager dangerManager;


	// Use this for initialization
	void Start () {
        playerResources.gameManager = this;
        resourcesHUD.gameManager = this;
        pointsAction.gameManager = this;
        workers.gameManager = this;
        globalObjectives.gameManager = this;
        upgradeManager.gameManager = this;
        dangerManager.gameManager = this;

        resourcesHUD.UpdateHUDAllResources();
	}


    // Update is called once per frame
    void Update () {
		
	}


    //HUD Update Methods
    public void UpdateResourcesHUD(ResourceType r)
    {
        resourcesHUD.UpdateHUD(r);
    }

    public void UpdatePointsActionHUD()
    {
        resourcesHUD.UpdatePointsActionHUD();
    }

    //Ressource Getters
    public int GetCurrentPointsAction()
    {
        return pointsAction.Points;
    }

    public int GetRessourceCount(ResourceType r)
    {
        return playerResources.GetCurrentResource(r);
    }

    //resource Setters
    public void RemovePointsAction(int change)
    {
        pointsAction.RemovePointsAction(change);
    }

    public void AddPlayerResources(ResourceType r, int amount)
    {
        playerResources.AddResources(r, amount);
        if(r == ResourceType.Workers)
        {
            workers.CreateNewWorkers(amount);
        }
    }

    public void RemovePlayerRessources(ResourceType r, int amount)
    {
        playerResources.RemoveResources(r, amount);
    }

    //End of year
    public void EndOfYear()
    {
        resourcesHUD.gameObject.SetActive(false);
        endOfYearUI.gameObject.SetActive(true);
        endOfYearUI.EndOfYear();
    }

    public bool VerifyAllObjectives()
    {
        return globalObjectives.VerifyAllObjectives();
    }
}
