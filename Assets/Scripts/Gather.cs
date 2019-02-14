using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : HexInteractable
{

    private GameManager gameManager;
    public ResourceType type;
    public int resourceValue;
    public int resourceMax;
    private int resourceCurrent;
    public int paCost;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        resourceCurrent = resourceMax;
    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        

        if (gameManager.GetCurrentPointsAction() >= paCost)
        {
            gameManager.RemovePointsAction(paCost);
            Debug.Log("You lost " + paCost + " points. You now have " + gameManager.GetCurrentPointsAction() + " points!");
            gameManager.AddPlayerResources(type, resourceValue);

            //si c'est 0 ou plus bas, tu peux rien ramasser.
            resourceCurrent -= resourceValue;
            if (resourceCurrent <= 0)

            {
                cell.SpecialIndex = 0;

            }
        }
    }

}
