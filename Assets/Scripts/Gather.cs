using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : HexInteractable {

    private PlayerResources playerResources;
    public ResourceType type;
    public int resourceValue;
    public int resourceMax;
    private int resourceCurrent;

    void Start()
    {
        playerResources = GameObject.FindObjectOfType<PlayerResources>();
        resourceCurrent = resourceMax;
    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        playerResources.AddResources(type, resourceValue);
        resourceCurrent -= resourceValue;


        //si c'est 0 ou plus bas, tu peux rien ramasser.
        if (resourceCurrent <= 0)

        {
            cell.SpecialIndex = 0;

        }
          
    }

}
