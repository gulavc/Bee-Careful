using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectives : MonoBehaviour
{

    [Header("Yearly Objectives (NERP)")]
    public Objective[] objectives;

    Dictionary<ResourceType, int> goals = new Dictionary<ResourceType, int>();
    [HideInInspector] public GameManager gameManager;

    public void Start()
    {
        //Initialize resources;
        goals.Add(ResourceType.Nectar, 0);
        goals.Add(ResourceType.Water, 0);
        goals.Add(ResourceType.Resin, 0);
        goals.Add(ResourceType.Pollen, 0);
        goals.Add(ResourceType.Workers, 0);

    }

    public void SetObjective(ResourceType r, int goal)
    {
        goals[r] = goal;
    }

    public void SetObjectivesByYear(int year)
    {
        Objective o = objectives[year];
        SetObjective(ResourceType.Nectar, o.NERP[0]);
        SetObjective(ResourceType.Water, o.NERP[1]);
        SetObjective(ResourceType.Resin, o.NERP[2]);
        SetObjective(ResourceType.Pollen, o.NERP[3]);
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

    //Objective Class
    [System.Serializable]
    public class Objective
    {
        public int[] NERP;
        public int Length {
            get {
                return NERP.Length;
            }
        }

        public Objective(int count)
        {
            NERP = new int[count];
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        int NumResources = FindObjectOfType<GameManager>().NumResources;
        int NumYears = FindObjectOfType<GameManager>().numberOfYears;
        if (objectives.Length != NumYears)
        {
            System.Array.Resize(ref objectives, NumYears);
        }

        for(int i = 0; i < objectives.Length; i++)
        {
            if(objectives[i].Length != NumResources)
            {
                objectives[i] = new Objective(NumResources);
            }
        }

    }
#endif

}
