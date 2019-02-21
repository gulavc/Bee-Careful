using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workers : MonoBehaviour {

    public GameManager gameManager;
    private List<int> workersEnergy;

    //public int MaxWorkerActionPoints;
    public ParticleSystem workerParticles;

	// Use this for initialization
	void Start () {
        //workersEnergy = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void CreateNewWorkers(int count = 1)
    {
        /*
        for(int i = 0; i < count; i++)
        {
            workersEnergy.Add(MaxWorkerActionPoints);
        }
        PrintWorkers();*/
    }

    public int[] GetAllWorkers()
    {
        return workersEnergy.ToArray();
    }

    public void UseWorker(int index, int energyUse)
    {
        if(index < workersEnergy.Count && index >= 0)
        {
            workersEnergy[index] -= energyUse;
            if (workersEnergy[index] <= 0)
            {
                workersEnergy.RemoveAt(index);
                gameManager.RemovePlayerRessources(ResourceType.Workers, 1);
            }
            workersEnergy.Sort();
        }
        
    }

    //Debug method
    public void PrintWorkers()
    { /*
        string output = "[";
        foreach(int energy in GetAllWorkers())
        {
            output += energy + ", ";
        }
        char[] toTrim = { '.', ' ' };
        output.TrimEnd(toTrim);
        output += "]";
        Debug.Log(output);*/
    }
}
