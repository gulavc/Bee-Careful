using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public GameManager gameManager;

    Dictionary<ResourceType, int> playerResources = new Dictionary<ResourceType, int>();
    public int maxResources = 100;

    public void Awake()
    {

        playerResources.Add(ResourceType.Nectar, 0);
        playerResources.Add(ResourceType.Water, 0);
        playerResources.Add(ResourceType.Resin, 0);
        playerResources.Add(ResourceType.Pollen, 0);
        playerResources.Add(ResourceType.Workers, 0);

    }

    public void AddResources(ResourceType r, int value)
    {
        if (ValidateResources(value))
        {
            playerResources[r] += value;

        }

        else
        {

            //Dealing with damage -- RIP
            /*if (r == ResourceType.Damage)
            {

                for (int i = 0; i < value; i++)
                {
                    if (ValidateResources(1))
                    {
                        playerResources[r] += 1;
                    }

                    else
                    {
                        bool done = false;

                        do
                        {

                            ResourceType result = (ResourceType)Random.Range(0, 4);
                            if (playerResources[result] > 0)
                            {
                                playerResources[result] -= 1;
                                playerResources[r] += 1;
                                done = true;
                            }
                            if (playerResources[r] == maxResources)
                            {
                                done = true;
                                Debug.Log("u dead bzz bzz");
                            }

                        } while (!done);



                    }
                }
            }*/


            int total = 0;
            foreach (int i in playerResources.Values)
            {
                total += i;
            }
            playerResources[r] += maxResources - total;


        }


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

    public bool ValidateResources(int newValue = 0)
    {
        int total = newValue;
        foreach (int i in playerResources.Values)
        {
            total += i;
        }

        return total <= maxResources;
    }

    public int GetCurrentResource(ResourceType r)
    {
        return playerResources[r];
    }

}
