using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public static bool CheapScoutUpgrade1 = false;
    public static bool CheapScoutUpgrade2 = false;
    public static bool CheapScoutUpgrade3 = false;

    public static bool CheapWorkUpgrade1 = false;
    public static bool CheapWorkUpgrade2 = false;
    public static bool CheapWorkUpgrade3 = false;

    public HexGrid hexGrid;
    [HideInInspector]
    public GameManager gameManager;
    public int scoutNectarCost;
    public int scoutPollenCost;
    public int workNectarCost;
    public int workPollenCost;
    public int addWorkers;

    
    public float WorkerDiscout {
        get {
            if (CheapWorkUpgrade3)
            {
                return 0.75f;
            }
            else if (CheapWorkUpgrade2)
            {
                return 0.5f;
            }
            else if (CheapWorkUpgrade1)
            {
                return 0.25f;
            }
            else
            {
                return 0f;
            }
        }
    }

    public float ScoutDiscout {
        get {
            if (CheapScoutUpgrade3)
            {
                return 0.75f;
            }
            else if (CheapScoutUpgrade2)
            {
                return 0.5f;
            }
            else if (CheapScoutUpgrade1)
            {
                return 0.25f;
            }
            else
            {
                return 0f;
            }
        }
    }

    public void CreateScout()
    {

        CreateScout(true, true);

    }

    public void CreateScout(bool addCount = true, bool paycost = true)
    {
        bool addUnit = true;
        if (paycost)
        {
            int actualNectarCost = (int)(scoutNectarCost * (1 - ScoutDiscout));
            int actualPollenCost = (int)(scoutPollenCost * (1 - ScoutDiscout));
            if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= actualNectarCost) && (gameManager.GetRessourceCount(ResourceType.Pollen) >= actualPollenCost))
            {
                gameManager.RemovePlayerRessources(ResourceType.Nectar, actualNectarCost);
                gameManager.RemovePlayerRessources(ResourceType.Pollen, actualPollenCost);
            }
            else
            {
                addUnit = false;
            }
        }
        if (addUnit)
        {
            HexCell cell = hexGrid.GetNearestEmptyCell(gameManager.HiveCell);
            if (cell)
            {

                if (addCount)
                {
                    gameManager.ScoutCount += 1;
                }
                hexGrid.AddUnit(Instantiate(HexUnit.unitPrefab), cell, Random.Range(0f, 360f));
            }
            else
            {
                Debug.Log("No empty cell left on map");
            }          
            
        }
    }


    public void CreateWorker()
    {
        int actualNectarCost = (int)(workNectarCost * (1 - WorkerDiscout));
        int actualPollenCost = (int)(workPollenCost * (1 - WorkerDiscout));

        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= actualNectarCost) && (gameManager.GetRessourceCount(ResourceType.Pollen) >= actualPollenCost))
        {

            gameManager.RemovePlayerRessources(ResourceType.Nectar, actualNectarCost);
            gameManager.RemovePlayerRessources(ResourceType.Pollen, actualPollenCost);
            gameManager.playerResources.AddResources(ResourceType.Workers, addWorkers);
        }

    }
}


