using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveResources : MonoBehaviour
{

    Dictionary<ResourceType, int> hiveResources = new Dictionary<ResourceType, int>();

    private void Start()
    {

    
       hiveResources.Add(ResourceType.Nectar, 0);
       hiveResources.Add(ResourceType.Water, 0);
       hiveResources.Add(ResourceType.Resin, 0);
       hiveResources.Add(ResourceType.Pollen, 0);
       hiveResources.Add(ResourceType.Bread, 0);
       hiveResources.Add(ResourceType.Honey, 0);
       hiveResources.Add(ResourceType.Jelly, 0);
       hiveResources.Add(ResourceType.Propolis, 0);
       hiveResources.Add(ResourceType.Wax, 0);
       
        
    }

public void AddResources(ResourceType r, int value)
    {

        hiveResources[r] = value;

    }



}
