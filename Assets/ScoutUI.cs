using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutUI : MonoBehaviour {

    public ParticleSystem workersPrefab;
    [HideInInspector]
    public HexCell currentCell;
    [HideInInspector]
    public ResourcePoint resourcePoint;
    [HideInInspector]
    public GameManager gameManager;

    private HexCell hiveCell;
    

    public void SummonWorker()
    {  

        resourcePoint = GetResourcePoint();

        if (resourcePoint)
        {
            resourcePoint.GatherResources();
            StartCoroutine(WorkersTravel());
        }
        else
        {
            Debug.Log("WTF");
        }
        
    }

    IEnumerator WorkersTravel()
    {
        if (!hiveCell)
        {
            hiveCell = gameManager.HiveCell;
        }

        ParticleSystem workers = Instantiate(workersPrefab);
        workers.transform.position = hiveCell.transform.position;
        workers.Play();

        Vector3 velocity = Vector3.zero;
        float smoothTime = 2f;

        //Go to resource point
        while(Vector3.Distance(workers.transform.position, resourcePoint.transform.position) > 0.1)
        {
            workers.transform.position = Vector3.SmoothDamp(workers.transform.position, resourcePoint.transform.position, ref velocity, smoothTime);
            yield return null;
        }

        //Gather for a couple seconds
        yield return new WaitForSeconds(4f);

        //Go back to hive
        while (Vector3.Distance(workers.transform.position, hiveCell.transform.position) > 0.1)
        {
            workers.transform.position = Vector3.SmoothDamp(workers.transform.position, hiveCell.transform.position, ref velocity, smoothTime);
            yield return null;
        }

        //Kill workers
        Destroy(workers.gameObject);
        
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
