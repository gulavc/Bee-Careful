using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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


    void Start()
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

}
