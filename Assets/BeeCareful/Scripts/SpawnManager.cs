using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    //public Button SpawnScout;
    //public Button SpawnWorker;
    public HexGrid hexGrid;
    [HideInInspector]
    public GameManager gameManager;
    public int scoutNectarCost;
    public int scoutPollenCost;
    public int workNectarCost;
    public int workPollenCost;
    public int addWorkers;

    // Use this for initialization
    void Start()
    {

    }
    void Update()

    {
        
    }



    public void CreateScout(bool addCount = true, bool paycost = true)
    {
        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= scoutNectarCost) && (gameManager.GetRessourceCount(ResourceType.Pollen) >= scoutPollenCost))
        {
            
            HexCell cell = hexGrid.GetNearestEmptyCell(gameManager.HiveCell);
            if (cell)
            {
                gameManager.RemovePlayerRessources(ResourceType.Nectar, scoutNectarCost);
                gameManager.RemovePlayerRessources(ResourceType.Pollen, scoutPollenCost);
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

        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= workNectarCost) && (gameManager.GetRessourceCount(ResourceType.Pollen) >= workPollenCost))
        {

            gameManager.RemovePlayerRessources(ResourceType.Nectar, workNectarCost);
            gameManager.RemovePlayerRessources(ResourceType.Pollen, workPollenCost);
            gameManager.playerResources.AddResources(ResourceType.Workers, addWorkers);
            Debug.Log("Let's gather some shit!");

        }

    }
}


