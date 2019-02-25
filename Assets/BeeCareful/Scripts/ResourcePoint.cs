using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : MonoBehaviour {

    private GameManager gameManager;
    public ResourceType type;
    //public int resourceValue;
    public int resourceMax;
    public int workforceCost;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        RemainingResources = resourceMax;
    }

    
    public void GatherResources(HexCell cell) {

        if (gameManager.GetCurrentPointsAction() >= workforceCost)
        {
            int resourceGet = Mathf.CeilToInt(RemainingResources / 2f);

            gameManager.RemovePlayerRessources(ResourceType.Workers, workforceCost);
            gameManager.AddPlayerResources(type, resourceGet);

            //si c'est 0 ou plus bas, tu peux rien ramasser.
            RemainingResources -= resourceGet;
            if (RemainingResources <= 0)
            {
                cell.SpecialIndex = 0;

            }
        }

    }

    public int RemainingResources { get; private set; }

}
