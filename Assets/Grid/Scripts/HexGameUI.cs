﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class HexGameUI : MonoBehaviour
{

    public GameObject HiveUI;
    public HexGrid grid;
    public ScoutUI scoutUI;

    [Header("Music")]
    public AudioClip hiveMusic;
    public MultiAudioClip moveSFX;
    public AudioClip swarmSFX;
    public AudioClip citySFX;
    public AudioClip riverSFX;

    private GameManager gameManager;    

    HexCell currentCell;

    HexUnit selectedUnit;
    public HexUnit GetSelectedUnit {
        get {
            return selectedUnit;
        }
    }

    List<int> resourcePointIndices;
    const int HiveSpecialIndex = 3;

    public int HiveIndex {
        get {
            return HiveSpecialIndex;
        }
    }

    [Header("Pointer Textures")]
    public Texture2D normalPointer;
    public Texture2D selectionPointer;
    public Texture2D movePointer;
    public Texture2D noMovePointer;
    public Vector2 pointerOffset = Vector2.zero;


    public bool PlayerHasControl {
        get; set;
    } = true;

    public int MoveToWorkerConversionRatio = 1;
    private float partialWorker;

    void Update()
    {
        

        if (PlayerHasControl && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                        if(Tutorial.earlyGameTutorial && gameManager.HiveCell.coordinates.DistanceTo(currentCell.coordinates) > 13)
                        {
                            gameManager.ShowTooFarTutorial();
                        }
                        else
                        {
                            DoMove();
                        }                        
                    }
                }
            }
            else if (selectedUnit)
            {
                DoPathfinding();
                if (Input.GetMouseButtonDown(1))
                {
                    DeselectUnit();
                }
            }
            else
            {
                Cursor.SetCursor(normalPointer, pointerOffset, CursorMode.Auto);
                if (currentCell)
                {
                    currentCell.DisableHighlight();
                }
                
                UpdateCurrentCell();
                if (currentCell && currentCell.Unit)
                {
                    currentCell.EnableHighlight(Color.white);
                    Cursor.SetCursor(selectionPointer, pointerOffset, CursorMode.Auto);
                }
            }
            //Debug.Log(currentCell.coordinates.ToString());
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

        PlayAmbiantSound();

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

        partialWorker = 0;

        Cursor.SetCursor(normalPointer, pointerOffset, CursorMode.Auto);
    }

    public void SetEditMode(bool toggle)
    {
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

    bool UpdateCurrentCell()
    {
        HexCell cell =
            grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (cell != currentCell)
        {
            currentCell = cell;
            return true;
        }
        return false;
    }

    void DoSelection()
    {
        grid.ClearPath();
        grid.ClearShowMovement();
        UpdateCurrentCell();
        if (currentCell)
        {
            selectedUnit = currentCell.Unit;
            if (currentCell.Unit)
            {
                currentCell.EnableHighlight(Color.blue);

                if (resourcePointIndices.Contains(currentCell.SpecialIndex))
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
        StopCoroutine(WaitForEndOfMove());
        if (!Tutorial.earlyGameTutorial && selectedUnit)
        {
            grid.ClearPath();
            grid.ClearShowMovement();
            selectedUnit.Location.DisableHighlight();
            selectedUnit = null;
            gameManager.ResetSeasonTimerPreview();
            HideScoutUI();
            Cursor.SetCursor(normalPointer, pointerOffset, CursorMode.Auto);
        }
    }

    void DoPathfinding()
    {

        if (UpdateCurrentCell())
        {
            if (selectedUnit.IsControllable)
            {
                if (currentCell && selectedUnit.IsValidDestination(currentCell))
                {
                    Cursor.SetCursor(movePointer, pointerOffset, CursorMode.Auto);
                    grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
                    gameManager.PreviewSeasonTimer(currentCell.Distance);                    
                }
                else
                {
                    Cursor.SetCursor(noMovePointer, pointerOffset, CursorMode.Auto);
                    grid.ClearPath();
                    selectedUnit.Location.EnableHighlight(Color.blue);
                    gameManager.ResetSeasonTimerPreview();                    
                }
            }
        }
    }

    void DoMove()
    {
        if (selectedUnit.IsControllable)
        {
            if (grid.HasPath)
            {
                gameManager.PlaySFX(moveSFX.GetRandomSound());

                selectedUnit.Travel(grid.GetPath());
                gameManager.RemovePointsAction(currentCell.Distance);

                float addWorker = currentCell.Distance / (float)HexUnit.MoveCost;
                float reminder = addWorker % 1f;
                partialWorker += reminder;
                if(partialWorker >= 1)
                {
                    addWorker++;
                    partialWorker--;
                }

                gameManager.AddPlayerResources(ResourceType.Workers, (int)addWorker);

                grid.ClearPath();
                StartCoroutine(WaitForEndOfMove());
            }
        }

    }

    void ShowPossibleMovement()
    {
        if (currentCell && currentCell.Unit)
        {
            grid.ClearShowMovement();
            grid.ShowPossibleMovement(selectedUnit.Location, currentCell, selectedUnit.Speed);
        }
    }

    public void ShowScoutUI()
    {        
        scoutUI.gameObject.SetActive(true);
        scoutUI.ShowButton();
        scoutUI.currentCell = currentCell;
        scoutUI.resourcePoint = gameManager.FindResourcePoint(currentCell);
        HexMapCamera.MoveTo(currentCell);
    }

    void HideScoutUI()
    {
        scoutUI.HideButton();
    }

    public void ShowHiveUI()
    {
        if (HiveUI.activeSelf)
        {
            HiveUI.SetActive(false);
            gameManager.StopMusic();
        }
        else
        {
            if (Tutorial.hiveEnabled)
            {
                DeselectUnit();
                HiveUI.SetActive(true);
                gameManager.PlayMusic(hiveMusic);

            }
        }        
    }

    IEnumerator WaitForEndOfMove()
    {

        while (selectedUnit && !selectedUnit.IsControllable)
        {
            yield return new WaitForEndOfFrame();

        }

        if (!selectedUnit)
        {
            yield break;
        }

        currentCell = selectedUnit.Location;   
        

        if (resourcePointIndices.Contains(currentCell.SpecialIndex) && Tutorial.gatherEnabled)
        {
            ShowScoutUI();            
        }
        else
        {
            HideScoutUI();
        }

        

    }

    public void SetCurrentCell(HexCell cell)
    {
        currentCell = cell;
    }

    private void PlayAmbiantSound()
    {
        if (selectedUnit)
        {
            HexCell cell = grid.GetCell(selectedUnit.transform.position);
            ResourcePoint rp = gameManager.rpManager.GetResourcePointByCell(cell);
            if(rp && rp.hasWasp)
            {
                gameManager.PlayAmbiance(swarmSFX);
            }
            else if (cell.UrbanLevel > 0)
            {
                gameManager.PlayAmbiance(citySFX);
            }
            else if (cell.HasRiver || cell.IsUnderwater)
            {
                gameManager.PlayAmbiance(riverSFX);
            }
            else
            {
                gameManager.StopAmbiance();
            }
        }
        else
        {
            gameManager.StopAmbiance();
        }        
    }
}
    