﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {    

    public bool launchInEditor = true;

    [Header("Public References to Management Scripts")]
    public PlayerResources playerResources;
    public PointsAction pointsAction;
    public GlobalObjectives globalObjectives;
    public UpgradeManager upgradeManager;
    public HexGameUI gameController;
    public ResourcePointManager rpManager;
    public HexGrid grid;
    public SpawnManager spawnManager;
    public Tutorial tutorials;

    [Space(10)]
    [Header("Public References to UI Elements")]
    public ResourcesHUD resourcesHUD;
    public EndOfYearUI endOfYearUI;
    public GameObject editorUI, gameUI, gameButtons;
    public SaveLoadMenu loader;
    public ScoutUI scoutUI;
    public PauseUI pauseUI;

    [Space(10)]
    [Header("Game Settings")]
    public int numberOfYears;
    public string[] maps;


    //Properties
    public int CurrentYear { get; set; } = 0;

    private HexCell hiveCell;
    public HexCell HiveCell {
        get {
            if (hiveCell == null)
            {
                hiveCell = grid.FindCellBySpecialIndex(gameController.HiveIndex);
            }
            return hiveCell;
        }
        private set {
            hiveCell = value;
        }
    }

    int scoutCount;
    public int ScoutCount {
        get {
            return scoutCount;            
        }
        set {
            scoutCount = value;
            globalObjectives.UpdateObjectives();
        }
    }

    //Private stuff
    const int NUMRESOURCES = 4;
    public int NumResources {
        get {
            return NUMRESOURCES;
        }
    }
    


#if UNITY_EDITOR
    private void OnValidate()
    {
        if(numberOfYears < 0)
        {
            numberOfYears = 0;
        }
        if(maps.Length != numberOfYears)
        {
            System.Array.Resize(ref maps, numberOfYears);            
        }        
    }
#endif

    // Use this for initialization
    void Start () {
        playerResources.gameManager = this;
        resourcesHUD.gameManager = this;
        pointsAction.gameManager = this;
        globalObjectives.gameManager = this;
        upgradeManager.gameManager = this;
        rpManager.gameManager = this;
        scoutUI.gameManager = this;
        spawnManager.gameManager = this;
        tutorials.gameManager = this;

        resourcesHUD.UpdateHUDAllResources();        

        Debug.Log(launchInEditor);

        if (!launchInEditor)
        {
            StartGame(GameLoader.LoadMode);
        }
        else
        {
            StartGame(LoadMode.Edit);
        }
        
        

    }
    
    // Update is called once per frame
    void Update () {
        
	}    

    //HUD Update Methods
    public void UpdateResourcesHUD(ResourceType r)
    {
        resourcesHUD.UpdateHUD(r);
    }

    public void UpdatePointsActionHUD()
    {
        resourcesHUD.UpdatePointsActionHUD();
    }

    public void HideScoutUI()
    {
        scoutUI.HideButton();
    }

    public void ShowPauseMenu()
    {
        pauseUI.Show();
    }

    public void PreviewSeasonTimer(int distance)
    {
        resourcesHUD.PreviewSeasonTimer(distance);
    }

    public void ResetSeasonTimerPreview()
    {
        resourcesHUD.ResetSeasonTimerPreview();
    }

    //Ressource Getters
    public int GetCurrentPointsAction()
    {
        return pointsAction.Points;
    }

    public int GetPointsActionMax()
    {
        return pointsAction.pointsActionMax;
    }

    public int GetRessourceCount(ResourceType r)
    {
        return playerResources.GetCurrentResource(r);
    }

    //resource Setters
    public void RemovePointsAction(int change)
    {
        pointsAction.RemovePointsAction(change);
    }

    public void SetCurrentPointsAction(int value, bool overrideLimit = false)
    {
        pointsAction.SetPointsAction(value, overrideLimit);
    }

    public void AddPlayerResources(ResourceType r, int amount)
    {
        playerResources.AddResources(r, amount);
    }

    public void RemovePlayerRessources(ResourceType r, int amount)
    {
        playerResources.RemoveResources(r, amount);
    }

    //End of year
    public void EndOfYear()
    {
        resourcesHUD.gameObject.SetActive(false);
        gameButtons.SetActive(false);
        endOfYearUI.gameObject.SetActive(true);
        endOfYearUI.EndOfYear();
    }

    public bool VerifyAllObjectives()
    {
        return globalObjectives.VerifyAllObjectives();
    }

    public ResourcePoint FindResourcePoint(HexCell cell)
    {
        return rpManager.GetResourcePointByCell(cell);
    }

    //Start new game
    public void StartGame(LoadMode lm)
    {
        if (lm == LoadMode.Play)
        {
            gameController.SetEditMode(false);
            editorUI.SetActive(false);

            StartCoroutine(LoadGame());            

            grid.ResetExploration();
            upgradeManager.ResetAllUpgrades();

            globalObjectives.SetObjectivesByYear(0);
            playerResources.SetStartingWorkers();
            resourcesHUD.UpdateHUDAllResources();

            //Turn Grid ON
            pauseUI.SetShowGrid(true);

            spawnManager.CreateScout(true, false);

            //Snap camera to Hive
            HexMapCamera.MoveTo(HiveCell, true);

            //Show Beginning Tutorial
            ShowStartUpTutorial();
        }
        else /*if (GameLoader.LoadMode == LoadMode.Edit)*/ //For now we presume that not play is "edit"
        {
            gameController.SetEditMode(true);
            gameUI.SetActive(false);
            gameButtons.SetActive(false);
        }
    }

    IEnumerator LoadGame()
    {
        if (rpManager)
        {
            rpManager.Clear();
        }
        loader.Load(Path.Combine(Application.dataPath, "StreamingAssets", loader.saveFolder, maps[CurrentYear] + ".map"));
        yield return new WaitForEndOfFrame();
        rpManager.RemoveTappedResources();
        rpManager.AddDangersOnResourcePoints();
    }


    //Year Advancement
    public void AdvanceYear()
    {
        CurrentYear++;
        if(CurrentYear >= numberOfYears)
        {
            Debug.Log("END OF GAME");
        }
        else
        {
            //Clear current stuff
            gameController.DeselectUnit();

            //Set new Dangers
            rpManager.IncreaseDanger();

            //Load new map
            grid.SaveMapExploration();
            StartCoroutine(LoadGame());
            grid.LoadMapExploration();

            //Set new objectives
            globalObjectives.SetObjectivesByYear(CurrentYear);

            //Set workers
            playerResources.SetStartingWorkers();

            //Reset player values
            SetCurrentPointsAction(pointsAction.pointsActionMax);            

            //Spawn new scouts
            for(int i = 0; i < ScoutCount; i++)
            {
                spawnManager.CreateScout(false, false);
            }

            //Move Camera to Hive
            HexMapCamera.MoveTo(HiveCell, true);

            //Activate UI
            resourcesHUD.gameObject.SetActive(true);
            gameButtons.SetActive(true);
        }
    }

    public void ChangeSeason(string newSeason)
    {
        //HUD.UpdateSeasonHud(newSeason);
        AddPassiveResourceBonus();
        Debug.Log("We are now in " + newSeason);
    }

    private void AddPassiveResourceBonus()
    {
        ResourceType t = ResourceType.Nectar;
        playerResources.AddResources(t, rpManager.GetPassiveBonus(t));

        t = ResourceType.Water;
        playerResources.AddResources(t, rpManager.GetPassiveBonus(t));

        t = ResourceType.Resin;
        playerResources.AddResources(t, rpManager.GetPassiveBonus(t));

        t = ResourceType.Pollen;
        playerResources.AddResources(t, rpManager.GetPassiveBonus(t));
    }

    public void RemovePlayerControls()
    {
        gameController.PlayerHasControl = false;
    }
    
    public void RestorePlayerControls()
    {
        gameController.PlayerHasControl = true;
    }

    //Tutorials
    public void ShowStartUpTutorial()
    {
        tutorials.ShowStartUpTutorial();
    }

    public void ShowMountainTutorial()
    {
        tutorials.ShowMountainTutorial();
    }

    public void ShowWaspsTutorial()
    {
        tutorials.ShowWaspsTutorial();   
    }

    public void ShowPesticideTutorial()
    {
        tutorials.ShowPesticideTutorial();
    }

    public void ShowTooFarTutorial()
    {
        tutorials.ShowTooFarTutorial();
    }
}
