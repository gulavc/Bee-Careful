using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectives : MonoBehaviour {

    Dictionary<ResourceType, int> goals = new Dictionary<ResourceType, int>();
    private PlayerResources ressources;

    public void Start()
    {
        ressources = GameObject.FindObjectOfType<PlayerResources>();
        goals.Add(ResourceType.Nectar, 0);
        goals.Add(ResourceType.Water, 0);
        goals.Add(ResourceType.Resin, 0);
        goals.Add(ResourceType.Pollen, 0);
        goals.Add(ResourceType.Damage, 0);

        //Temporary stuff to test
        SetObjective(ResourceType.Nectar, 15);
        SetObjective(ResourceType.Water, 15);
        SetObjective(ResourceType.Resin, 15);
        SetObjective(ResourceType.Pollen, 15);

    }

    public void SetObjective(ResourceType r, int goal)
    {
        goals[r] = goal;
    }


    public bool  VerifyObjective(ResourceType r)
    {
        return ressources.GetCurrentResource(r) >= goals[r];
    }        


    public int GetObjective(ResourceType r)
    {
        return goals[r];
    }

    //Temp code to test
    /*private void Update()
    {
        if (VerifyObjective(ResourceType.Water) && 
    }*/
}
