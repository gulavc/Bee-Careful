using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : HexInteractable
{

    private PlayerResources playerResources;
    public ResourceType type;
    public int resourceValue;
    public int resourceMax;
    private int resourceCurrent;
    private PointsAction pa;
    public int paCost;

    void Start()
    {
        playerResources = GameObject.FindObjectOfType<PlayerResources>();
        pa = GameObject.FindObjectOfType<PointsAction>();
        resourceCurrent = resourceMax;
    }

    public override void OnUnitEnterCell(HexCell cell)
    {


        if (pa.pointsAction > paCost)
        {
            pa.pointsAction -= paCost;
            pa.SetCountText();
            Debug.Log("You lost " + paCost + " points. You know have " + pa.pointsAction + " points!");
            playerResources.AddResources(type, resourceValue);

            //si c'est 0 ou plus bas, tu peux rien ramasser.
            resourceCurrent -= resourceValue;
            if (resourceCurrent <= 0)

            {
                cell.SpecialIndex = 0;

            }
        }
    }

}
