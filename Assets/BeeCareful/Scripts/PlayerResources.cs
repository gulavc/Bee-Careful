using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

    public static bool StartWorkUpgrade1 = false;
    public static bool StartWorkUpgrade2 = false;
    public static bool StartWorkUpgrade3 = false;

    [HideInInspector] public GameManager gameManager;
    private UpgradeAlert upgradeAlert;

    Dictionary<ResourceType, int> playerResources = new Dictionary<ResourceType, int>();

    private int StartingWorkers {
        get {
            if (StartWorkUpgrade3)
            {
                return 200;
            }
            else if (StartWorkUpgrade2)
            {
                return 150;
            }
            else if (StartWorkUpgrade1)
            {
                return 100;
            }
            else
            {
                return 50;
            }
        }
    }

    public void Awake()
    {
        //Here's to change the start ressources number
        playerResources.Add(ResourceType.Nectar, 0);
        playerResources.Add(ResourceType.Water, 0);
        playerResources.Add(ResourceType.Resin, 0);
        playerResources.Add(ResourceType.Pollen, 0);
        playerResources.Add(ResourceType.Workers, 0);

    }

    public void AddResources(ResourceType r, int value)
    {
        //TODO: ADD ANIMATION TO INDICATE THIS;
        playerResources[r] += value;
        gameManager.UpdateResourcesHUD(r);

        //Upgrade Pop-up HERE
        if (!upgradeAlert)
        {
            upgradeAlert = FindObjectOfType<UpgradeAlert>();
        }
        if (upgradeAlert.VerifyPopup())
        {
            upgradeAlert.Show();
        }

    }

    public void RemoveResources(ResourceType r, int amount)
    {
        //TODO: ADD ANIMATION TO INDICATE THIS;
        if (amount >= 0)
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

    public void SetStartingWorkers()
    {
        playerResources[ResourceType.Workers] = StartingWorkers;
        gameManager.UpdateResourcesHUD(ResourceType.Workers);
    }
}
