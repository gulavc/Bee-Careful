using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPointsAction : HexInteractable {

    private PointsAction pa;

    void Start()
    {
        pa = GameObject.FindObjectOfType<PointsAction>();
    }

    public override void OnUnitEnterCell(HexCell cell)
    {

        pa.pointsAction -= 1;
        pa.SetCountText();

    }

}
