using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class HexGameUI : MonoBehaviour
{

    public GameObject HiveUI;
    public HexGrid grid;
    public ScoutUI scoutUI;
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
                if (currentCell)
                {
                    currentCell.DisableHighlight();
                }
                
                UpdateCurrentCell();
                if (currentCell.Unit)
                {
                    currentCell.EnableHighlight(Color.white);
                }
            }
            Debug.Log(currentCell.coordinates.ToString());
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

        partialWorker = 0;
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
        if (selectedUnit)
        {
            grid.ClearPath();
            grid.ClearShowMovement();
            selectedUnit.Location.DisableHighlight();
            selectedUnit = null;
            gameManager.ResetSeasonTimerPreview();
            HideScoutUI();
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
                    grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
                    gameManager.PreviewSeasonTimer(currentCell.Distance);
                }
                else
                {
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

    void ShowScoutUI()
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
        if (Tutorial.hiveEnabled)
        {
            HiveUI.SetActive(true);
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
}
    