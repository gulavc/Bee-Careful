using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : HexInteractable
{
    public override void OnUnitEnterCell(HexCell cell)
    {
        Debug.Log("Trigger");
    }

    
}
