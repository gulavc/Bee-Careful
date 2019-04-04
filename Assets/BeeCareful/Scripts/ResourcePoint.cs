using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : HexInteractable {

    private GameManager gameManager;
    private ResourcePointManager rpm;
    public ResourceType type;
    //public int resourceValue;
    public int resourceMax;
    public int workforceCost;

    public bool hasWasp = false;
    public bool hasPesticide = false;
    
    [HideInInspector]
    public GameObject dangerPrefab;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rpm = GameObject.FindObjectOfType<ResourcePointManager>();
        
        rpm.AddResourcePoint(this);
        RemainingResources = resourceMax;

        Cell = gameManager.grid.GetCell(HexCoordinates.FromPosition(transform.position));

    }

    protected override void Update()
    {
        base.Update();

        if (dangerPrefab)
        {
            if (Cell.IsVisible)
            {
                dangerPrefab.SetActive(true);
                HideGoupille();
            }
            else
            {
                dangerPrefab.SetActive(false);
                ShowGoupille();
            }
        }
    }


    public void GatherResources() {

        int actualWorkforceCost = workforceCost;
        if (hasPesticide)
        {
            actualWorkforceCost = (int)(actualWorkforceCost * rpm.pesticidePenalty);
        }

        if (gameManager.GetRessourceCount(ResourceType.Workers) >= actualWorkforceCost)
        {
            int resourceGet = Mathf.CeilToInt(RemainingResources / 2f);
            RemainingResources -= resourceGet;
            //si c'est 0 ou plus bas, tu peux rien ramasser.            
            if (RemainingResources <= 0)
            {
                Cell.SpecialIndex = 0;
                gameManager.HideScoutUI();
            }

            if (hasWasp)
            {
                resourceGet = (int)(resourceGet * (1 - rpm.waspPenalty)); 
            }
            

            gameManager.RemovePlayerRessources(ResourceType.Workers, actualWorkforceCost);
            gameManager.AddPlayerResources(type, resourceGet);

            
        }

    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        if (hasWasp)
        {
            gameManager.ShowWaspsTutorial();
        }
        if (hasPesticide)
        {
            gameManager.ShowPesticideTutorial();
        }
    }

    public void HideGoupille()
    {
        GetComponentInChildren<Goupille>().Hide();
    }

    public void ShowGoupille()
    {
        GetComponentInChildren<Goupille>().Show();
    }

    public int RemainingResources { get; private set; }

}
