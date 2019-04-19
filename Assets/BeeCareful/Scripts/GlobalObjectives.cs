using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectives : MonoBehaviour
{
    
    public static bool ObjectivesUpgrade1 = false;
    public static bool ObjectivesUpgrade2 = false;
    public static bool ObjectivesUpgrade3 = false;

    [Header("Yearly Objectives (NERP)")]
    public Objective[] objectives;

    Dictionary<ResourceType, int> goals = new Dictionary<ResourceType, int>();
    [HideInInspector] public GameManager gameManager;

    public int ObjectiveDiscount {
        get {
            if (ObjectivesUpgrade3)
            {
                return 3 * gameManager.ScoutCount;
            }
            else if (ObjectivesUpgrade2)
            {
                return 2 * gameManager.ScoutCount;
            }
            else if (ObjectivesUpgrade1)
            {
                return 1 * gameManager.ScoutCount;
            }
            else
            {
                return 0;
            }
        }
    }

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
        SetObjective(ResourceType.Nectar, o.NERP[0] - ObjectiveDiscount < 0 ? 0 : o.NERP[0] - ObjectiveDiscount);
        SetObjective(ResourceType.Water, o.NERP[1] - ObjectiveDiscount < 0 ? 0 : o.NERP[1] - ObjectiveDiscount);
        SetObjective(ResourceType.Resin, o.NERP[2] - ObjectiveDiscount < 0 ? 0 : o.NERP[2] - ObjectiveDiscount);
        SetObjective(ResourceType.Pollen, o.NERP[3] - ObjectiveDiscount < 0 ? 0 : o.NERP[3] - ObjectiveDiscount);
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

    public void UpdateObjectives()
    {
        SetObjectivesByYear(gameManager.CurrentYear);
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
