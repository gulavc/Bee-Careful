using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

    Dictionary<ResourceType, int> playerResources = new Dictionary<ResourceType, int>();
    public int maxResources = 50;
    public int moreNectar = 2;
    public int moreWater = 4;
    public int moreResin = 6;
    public int morePollen = 8;
    public int moreDamage = 10;

    public void Start()
    {

        playerResources.Add(ResourceType.Nectar, 0);
        playerResources.Add(ResourceType.Water, 0);
        playerResources.Add(ResourceType.Resin, 0);
        playerResources.Add(ResourceType.Pollen, 0);
        playerResources.Add(ResourceType.Damage, 0);

    }

    public void AddResources(ResourceType r, int value)
    {
        if (ValidateResources(value))
        {
            playerResources[r] += value;

        }

        else
        {

            if (r == ResourceType.Damage)
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
            }


            int total = 0;
            foreach (int i in playerResources.Values)
            {
                total += i;
            }
            playerResources[r] += maxResources - total;


        }


        Debug.Log(printRessources());

    }

    public string printRessources()
    {
        return ResourceType.Nectar + ": " + playerResources[ResourceType.Nectar] + " / " +
            ResourceType.Water + ": " + playerResources[ResourceType.Water] + " / " +
            ResourceType.Resin + ": " + playerResources[ResourceType.Resin] + " / " +
            ResourceType.Pollen + ": " + playerResources[ResourceType.Pollen] + " / " +
            ResourceType.Damage + ": " + playerResources[ResourceType.Damage];
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddResources(ResourceType.Nectar, moreNectar);
            Debug.Log("Got Nectar");

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddResources(ResourceType.Water, moreWater);
            Debug.Log("Got Water");

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AddResources(ResourceType.Resin, moreResin);
            Debug.Log("Got Resin");

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddResources(ResourceType.Pollen, morePollen);
            Debug.Log("Got Pollen");

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddResources(ResourceType.Damage, moreDamage);
            Debug.Log("Got Pollen");

        }

    }



}
