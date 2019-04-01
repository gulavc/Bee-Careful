﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HexGameUI : MonoBehaviour {

    public GameObject HiveUI;
    public HexGrid grid;
    public ScoutUI scoutUI;
    private GameManager gameManager;

    HexCell currentCell;

    HexUnit selectedUnit;

    List<int> resourcePointIndices;
    const int HiveSpecialIndex = 3;

    public int HiveIndex {
        get {
            return HiveSpecialIndex;
        }
    }

    void Update() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetMouseButtonDown(0)) {
                if (!selectedUnit)
                {
                    DoSelection();
                }
                else
                {
                    UpdateCurrentCell();
                    if (currentCell.Unit)
                    {
                        //DoSelection();
                    }
                    else
                    {
                        DoMove();
                    }                    
                }                
            }
            else if (selectedUnit) {
                DoPathfinding();
                if (Input.GetMouseButtonDown(1)) {
                    DeselectUnit();                  
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!selectedUnit)
            {
                gameManager.ShowPauseMenu();
            }
            else
            {
                DeselectUnit();
            }
        }
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //This contains all the special indices of the resource points
        resourcePointIndices = new List<int>();
        resourcePointIndices.Add(1);
        resourcePointIndices.Add(2);
        resourcePointIndices.Add(4);
        resourcePointIndices.Add(5);
    }

    public void SetEditMode(bool toggle) {
        enabled = !toggle;
        grid.ShowUI(!toggle);
        grid.ClearPath();
        if (toggle)
        {
            Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
        }
        else
        {
            Shader.DisableKeyword("HEX_MAP_EDIT_MODE");
        }
    }

    bool UpdateCurrentCell() {
        HexCell cell =
            grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (cell != currentCell) {
            currentCell = cell;
            return true;
        }
        return false;
    }

    void DoSelection() {
        grid.ClearPath();
        grid.ClearShowMovement();
        UpdateCurrentCell();
        if (currentCell) {
            selectedUnit = currentCell.Unit;
            if (currentCell.Unit)
            {
                currentCell.EnableHighlight(Color.blue);

                if(resourcePointIndices.Contains(currentCell.SpecialIndex))
                {
                    ShowScoutUI();
                }
                else
                {
                    HideScoutUI();
                }

            }
            else
            {
                HideScoutUI();
            }

            if (currentCell.SpecialIndex == HiveSpecialIndex)
            {
                ShowHiveUI();                
            }
        }
    }

    public void DeselectUnit()
    {
        if (selectedUnit)
        {
            grid.ClearPath();
            grid.ClearShowMovement();
            selectedUnit.Location.DisableHighlight();
            selectedUnit = null;
        }        
    }

    void DoPathfinding() {
        if (UpdateCurrentCell()) {
            if (currentCell && selectedUnit.IsValidDestination(currentCell)) {
                grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
                gameManager.PreviewSeasonTimer(currentCell.Distance);
            }
            else {                
                grid.ClearPath();
                selectedUnit.Location.EnableHighlight(Color.blue);
                gameManager.ResetSeasonTimerPreview();
            }
        }
    }

    void DoMove() {
        if (grid.HasPath /*&& grid.IsReachable(currentCell)*/) {
            selectedUnit.Travel(grid.GetPath());
            gameManager.RemovePointsAction(currentCell.Distance);
            grid.ClearPath();
            if (resourcePointIndices.Contains(currentCell.SpecialIndex))
            {
                ShowScoutUI();
            }
            else
            {
                HideScoutUI();
            }
        }
    }

    void ShowPossibleMovement() {
        if(currentCell && currentCell.Unit) {
            grid.ClearShowMovement();
            grid.ShowPossibleMovement(selectedUnit.Location, currentCell, selectedUnit.Speed);
        }
    }

    void ShowScoutUI()
    {
        scoutUI.gameObject.SetActive(true);
        scoutUI.currentCell = currentCell;
        scoutUI.resourcePoint = gameManager.FindResourcePoint(currentCell);
        HexMapCamera.MoveTo(currentCell);
    }

    void HideScoutUI()
    {
        scoutUI.gameObject.SetActive(false);
    }

    public void ShowHiveUI()
    {
        HiveUI.SetActive(true);
    }
}