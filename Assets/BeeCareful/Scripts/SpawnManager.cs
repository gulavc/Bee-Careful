using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int scoutWaterCost;
    public int scoutResinCost;
    public int scoutPollenCost;
    public int workNectarCost;
    public int workWaterCost;
    public int workResinCost;
    public int workPollenCost;
    public int addWorkers;

    [Header("Tooltips references")]
    public Text workerTooltip;
    public Text scoutTooltip;

    [Header("Sounds")]
    public AudioClip workerCreated;
    public AudioClip scoutCreated;
    public AudioClip notEnoughResources;


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
            int actualWaterCost = (int)(scoutWaterCost * (1 - ScoutDiscout));
            int actualResinCost = (int)(scoutResinCost * (1 - ScoutDiscout));
            int actualPollenCost = (int)(scoutPollenCost * (1 - ScoutDiscout));
            if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= actualNectarCost) && 
                (gameManager.GetRessourceCount(ResourceType.Pollen) >= actualPollenCost) && 
                (gameManager.GetRessourceCount(ResourceType.Water) >= actualWaterCost)&& 
                (gameManager.GetRessourceCount(ResourceType.Resin) >= actualResinCost))
            {
                gameManager.RemovePlayerRessources(ResourceType.Nectar, actualNectarCost);
                gameManager.RemovePlayerRessources(ResourceType.Water, actualWaterCost);
                gameManager.RemovePlayerRessources(ResourceType.Resin, actualResinCost);
                gameManager.RemovePlayerRessources(ResourceType.Pollen, actualPollenCost);

                gameManager.PlaySFX(scoutCreated);
            }
            else
            {
                addUnit = false;
                gameManager.PlaySFX(notEnoughResources);
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
        int actualWaterCost = (int)(workWaterCost * (1 - WorkerDiscout));
        int actualResinCost = (int)(workResinCost * (1 - WorkerDiscout));
        int actualPollenCost = (int)(workPollenCost * (1 - WorkerDiscout));

        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= actualNectarCost) && 
            (gameManager.GetRessourceCount(ResourceType.Pollen) >= actualPollenCost) && 
            (gameManager.GetRessourceCount(ResourceType.Water) >= actualWaterCost) && 
            (gameManager.GetRessourceCount(ResourceType.Resin) >= actualResinCost))
        {

            gameManager.RemovePlayerRessources(ResourceType.Nectar, actualNectarCost);
            gameManager.RemovePlayerRessources(ResourceType.Water, actualWaterCost);
            gameManager.RemovePlayerRessources(ResourceType.Resin, actualResinCost);
            gameManager.RemovePlayerRessources(ResourceType.Pollen, actualPollenCost);
            gameManager.playerResources.AddResources(ResourceType.Workers, addWorkers);

            gameManager.PlaySFX(workerCreated);
        }
        else
        {
            gameManager.PlaySFX(notEnoughResources);
        }

    }

    public int[] GetWorkerCost()
    {    
        int[] value = new int[4];

        value[0] = (int)(workNectarCost * (1 - WorkerDiscout));
        value[1] = (int)(workWaterCost * (1 - WorkerDiscout));
        value[2] = (int)(workResinCost * (1 - WorkerDiscout));
        value[3] = (int)(workPollenCost * (1 - WorkerDiscout));
        return value;
    }

    public void ShowWorkerToolip()
    {
        int[] costs = GetWorkerCost();

        workerTooltip.text = "Create new Workers to gather resources. We need " + costs[0] + " Nectar, " + costs[1] + " Water, " + costs[2] + " Resin and " + costs[3] + " Pollen."; 

        workerTooltip.gameObject.SetActive(true);

    }

    public int[] GetScoutCost()
    {
        int[] value = new int[4];

        value[0] = (int)(scoutNectarCost * (1 - ScoutDiscout));
        value[1] = (int)(scoutWaterCost * (1 - ScoutDiscout));
        value[2] = (int)(scoutResinCost * (1 - ScoutDiscout));
        value[3] = (int)(scoutPollenCost * (1 - ScoutDiscout));
        return value;
    }

    public void ShowScoutToolip()
    {
        int[] costs = GetScoutCost();

        scoutTooltip.text = "Create a new Scout to explore the world. We need " + costs[0] + " Nectar, " + costs[1] + " Water, " + costs[2] + " Resin and " + costs[3] + " Pollen.";

        scoutTooltip.gameObject.SetActive(true);

    }


}


