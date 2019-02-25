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
    

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        //rpm = GameObject.FindObjectOfType<ResourcePointManager>();

        //rpm.AddResourcePoint(this);
        RemainingResources = resourceMax;

    }

    
    public void GatherResources() {

        if (gameManager.GetCurrentPointsAction() >= workforceCost)
        {
            int resourceGet = Mathf.CeilToInt(RemainingResources / 2f);

            gameManager.RemovePlayerRessources(ResourceType.Workers, workforceCost);
            gameManager.AddPlayerResources(type, resourceGet);

            //si c'est 0 ou plus bas, tu peux rien ramasser.
            RemainingResources -= resourceGet;
            if (RemainingResources <= 0)
            {
                Cell.SpecialIndex = 0;

            }
        }

    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        //Do nothing
    }

    public int RemainingResources { get; private set; }

}
