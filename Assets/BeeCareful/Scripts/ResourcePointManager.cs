using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ResourcePointManager : MonoBehaviour {

    [Header("Danger Distribution")]
    [Range(0,100)]
    public int percentWasps;

    [Range(0, 100)]
    public int percentPesticide;

    [Space(10)]
    [Header("Danger Effectiveness")]
    [Range(0, 1)]
    public float waspPenalty;

    [Range(1, 2)]
    public float pesticidePenalty;

    [Space(10)]
    [Header("Prefabs references")]
    public GameObject waspPrefab;
    public GameObject pesticidePrefab;



    //Editor Script to limit the total danger % to 100
#if UNITY_EDITOR
    private void OnValidate()
    {
        if(percentPesticide + percentWasps > 100)
        {
            int finalPercentPesticide = (100 * percentPesticide) / (percentPesticide + percentWasps);
            int finalPercentWasps = (100 * percentWasps) / (percentPesticide + percentWasps);

            percentWasps = finalPercentWasps;
            percentPesticide = finalPercentPesticide;
        }

        if(pesticidePenalty < 1f)
        {
            pesticidePenalty = 1f;
        } 
        else if (pesticidePenalty > 2f)
        {
            pesticidePenalty = 2f;
        }

        if(waspPenalty < 0f)
        {
            waspPenalty = 0f;
        }
        else if (waspPenalty > 1f)
        {
            waspPenalty = 1f;
        }
    }
#endif

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

        /*foreach (ResourcePoint rp in FindObjectsOfType<ResourcePoint>())
        {
            AddResourcePoint(rp);
        }*/

        int count = resourcePoints.Count;
        int numPesticides = (int)(percentPesticide / 100f * count);
        int numWasps = (int)(percentWasps / 100f * count);

        resourcePoints = resourcePoints.OrderBy(a => rng.Next()).ToList();

        for(int i = 0; i < numPesticides; i++)
        {
            //Add pesticide on resource
            resourcePoints[i].hasPesticide = true;
            Instantiate(pesticidePrefab, resourcePoints[i].transform);
        }

        for (int i = numPesticides; i < numPesticides + numWasps; i++)
        {
            //Add wasp on resource
            resourcePoints[i].hasWasp = true;
            Instantiate(waspPrefab, resourcePoints[i].transform);
        }

        Debug.Log("RP: " + count + ", Wasp: " + numWasps + " / Pest: " + numPesticides);


    }

}
