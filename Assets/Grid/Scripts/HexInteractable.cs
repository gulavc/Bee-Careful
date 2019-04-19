using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HexInteractable : MonoBehaviour {

    public HexCell Cell { get; set; }

    private bool activateAction, actionDone;

    void Start()
    {
        activateAction = false;
        actionDone = false;
    }

    protected virtual void Update()
    {
        
        if (Cell.Unit)
        {
            activateAction = true;
        }
        else
        {
            activateAction = false;
            actionDone = false;
        }

        if (activateAction && !actionDone)
        {            
            OnUnitEnterCell(Cell);
            actionDone = true;
        }
    }

    public abstract void OnUnitEnterCell(HexCell cell);

}
