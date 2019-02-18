using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectives : MonoBehaviour
{

    Dictionary<ResourceType, int> goals = new Dictionary<ResourceType, int>();
    public GameManager gameManager;

    public void Start()
    {

        goals.Add(ResourceType.Nectar, 0);
        goals.Add(ResourceType.Water, 0);
        goals.Add(ResourceType.Resin, 0);
        goals.Add(ResourceType.Pollen, 0);
        goals.Add(ResourceType.Workers, 0);

        //Temporary stuff to test
        SetObjective(ResourceType.Nectar, 100);
        SetObjective(ResourceType.Water, 100);
        SetObjective(ResourceType.Resin, 100);
        SetObjective(ResourceType.Pollen, 100);

    }

    public void SetObjective(ResourceType r, int goal)
    {
        goals[r] = goal;
    }


    public bool VerifyObjective(ResourceType r)
    {
        return gameManager.GetRessourceCount(r) >= goals[r];
    }


    public int GetObjective(ResourceType r)
    {
        return goals[r];
    }

    public bool VerifyAllObjectives()
    {
        bool objectivesComplete = true;
        foreach (ResourceType r in goals.Keys)
        {
            if (goals[r] != 0 && !VerifyObjective(r))
            {
                objectivesComplete = false;
            }
        }

        return objectivesComplete;
    }

}
