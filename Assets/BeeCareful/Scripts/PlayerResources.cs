using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;

    Dictionary<ResourceType, int> playerResources = new Dictionary<ResourceType, int>();

    public void Awake()
    {
        //Here's to change the start ressources number
        playerResources.Add(ResourceType.Nectar, 0);
        playerResources.Add(ResourceType.Water, 0);
        playerResources.Add(ResourceType.Resin, 0);
        playerResources.Add(ResourceType.Pollen, 0);
        playerResources.Add(ResourceType.Workers, 200);

    }

    public void AddResources(ResourceType r, int value)
    {
        
        playerResources[r] += value;
        gameManager.UpdateResourcesHUD(r);

    }

    public void RemoveResources(ResourceType r, int amount)
    {
        if(amount >= 0)
        {
            playerResources[r] = playerResources[r] - amount < 0 ? 0 : playerResources[r] - amount;
        }
        gameManager.UpdateResourcesHUD(r);

    }

    public string printRessources()
    {
        return ResourceType.Nectar + ": " + playerResources[ResourceType.Nectar] + " / " +
            ResourceType.Water + ": " + playerResources[ResourceType.Water] + " / " +
            ResourceType.Resin + ": " + playerResources[ResourceType.Resin] + " / " +
            ResourceType.Pollen + ": " + playerResources[ResourceType.Pollen] + " / " +
            ResourceType.Workers + ": " + playerResources[ResourceType.Workers];
    }    

    public int GetCurrentResource(ResourceType r)
    {
        return playerResources[r];
    }
}
