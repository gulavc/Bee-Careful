using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    //public Button SpawnScout;
    //public Button SpawnWorker;
    private HexGrid hexGrid;
    private GameManager gameManager;
    public int scoutNectarCost;
    public int scoutPollenCost;
    public int workNectarCost;
    public int workPollenCost;
    public int addWorkers;

    // Use this for initialization
    void Start()
    {

        //SpawnScout.onClick.AddListener(CreateScout);
        /*SpawnWorker.onClick.AddListener(CreateWorker);*/
        hexGrid = GameObject.FindObjectOfType<HexGrid>();
        gameManager = GameObject.FindObjectOfType<GameManager>();


    }


    void Update()

    {
        
    }



    public void CreateScout()
    {
        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= scoutNectarCost) && (gameManager.GetRessourceCount(ResourceType.Pollen) >= scoutPollenCost))
        {

            gameManager.RemovePlayerRessources(ResourceType.Nectar, scoutNectarCost);
            gameManager.RemovePlayerRessources(ResourceType.Pollen, scoutPollenCost);
            Debug.Log("Let's explore this hood!");
            HexCell cell = hexGrid.GetCell(new Vector3(13, -30, 17));
            if (cell && !cell.Unit)
            {
                hexGrid.AddUnit(Instantiate(HexUnit.unitPrefab), cell, Random.Range(0f, 360f));

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


