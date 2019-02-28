using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutUI : MonoBehaviour {

    public ParticleSystem workersPrefab;
    [HideInInspector]
    public HexCell currentCell;
    [HideInInspector]
    public ResourcePoint resourcePoint;

    public void SummonWorker()
    {
        ParticleSystem workers = Instantiate(workersPrefab);
        workers.transform.position = currentCell.transform.position;
        workers.Play();
        Destroy(workers, 5f);

        resourcePoint = GetResourcePoint();

        if (resourcePoint)
        {
            resourcePoint.GatherResources();
        }
        else
        {
            Debug.Log("WTF");
        }
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    ResourcePoint GetResourcePoint()
    {
        foreach(ResourcePoint rp in GameObject.FindObjectsOfType<ResourcePoint>())
        {
            if(rp.Cell == currentCell)
            {
                return rp;
            }
        }

        return null;
    }

}
