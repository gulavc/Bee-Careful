using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerResources playerResources;
    public ResourcesHUD resourcesHUD;
    public PointsAction pointsAction;

	// Use this for initialization
	void Start () {
        playerResources.gameManager = this;
        resourcesHUD.gameManager = this;
        pointsAction.gameManager = this;

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
    }
}
