using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ResourcePointManager : MonoBehaviour {

    [Range(0,100)]
    public int percentWasps;

    [Range(0, 100)]
    public int percentPesticide;

    //Editor Script to limit the total danger % to 100
    private void OnValidate()
    {
        if(percentPesticide + percentWasps > 100)
        {
            int finalPercentPesticide = (100 * percentPesticide) / (percentPesticide + percentWasps);
            int finalPercentWasps = (100 * percentWasps) / (percentPesticide + percentWasps);

            percentWasps = finalPercentWasps;
            percentPesticide = finalPercentPesticide;
        }
    }

    [HideInInspector]
    public GameManager gameManager;

    private List<ResourcePoint> resourcePoints;

    void Awake()
    {
        resourcePoints = new List<ResourcePoint>();
    }


    public ResourcePoint GetResourcePointByCell(HexCell cell)
    {

        foreach(ResourcePoint r in resourcePoints)
        {
            if (r.Cell == cell)
            {
                return r;
            }
        }

        return null;

    }

    public void AddResourcePoint(ResourcePoint rp)
    {
        resourcePoints.Add(rp);
    }

    public void AddDangersOnResourcePoints()
    {
        System.Random rng = new System.Random();

        foreach (ResourcePoint rp in FindObjectsOfType<ResourcePoint>())
        {
            AddResourcePoint(rp);
        }

        int count = resourcePoints.Count;
        int numPesticides = (int)(percentPesticide / 100f * count);
        int numWasps = (int)(percentWasps / 100f * count);

        resourcePoints = resourcePoints.OrderBy(a => rng.Next()).ToList();

        for(int i = 0; i < numPesticides; i++)
        {
            //Add pesticide on resource
            resourcePoints[i].hasPesticide = true;
        }

        for (int i = numPesticides; i < numPesticides + numWasps; i++)
        {
            //Add wasp on resource
            resourcePoints[i].hasWasp = true;
        }

        Debug.Log("RP: " + count + ", Wasp: " + numWasps + " / Pest: " + numPesticides);


    }

}
