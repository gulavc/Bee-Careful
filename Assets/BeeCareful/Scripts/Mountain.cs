using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : HexInteractable {
    public GameObject[] nuages;
    public float rotationSpeed;

    public static bool mountainVisited = false;

    public override void OnUnitEnterCell(HexCell cell)
    {
        if (!mountainVisited)
        {
            HexMapCamera.MoveTo(cell);
            FindObjectOfType<GameManager>().ShowMountainTutorial();
            mountainVisited = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!Cell.IsVisible)
        {
            Cell.IncreaseVisibility();
        }

        foreach(GameObject g in nuages)
        {
            g.transform.RotateAround(this.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }

}
