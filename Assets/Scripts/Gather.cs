using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : HexInteractable {

    private PlayerResources playerResources;
    public ResourceType type;
    public int resourceValue;

    void Start()
    {
        playerResources = GameObject.FindObjectOfType<PlayerResources>();
    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        playerResources.AddResources(type, resourceValue);
        Debug.Log("Got something!");
    }

   

}
