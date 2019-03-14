using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

    public bool launchInEditor = true;

    [Header("Public References to Management Scripts")]
    public PlayerResources playerResources;    
    public PointsAction pointsAction;
    public Workers workers;    
    public GlobalObjectives globalObjectives;
    public UpgradeManager upgradeManager;
    public HexGameUI gameController;
    public ResourcePointManager rpManager;
    public HexGrid grid;

    [Space(10)]
    [Header("Public References to UI Elements")]
    public ResourcesHUD resourcesHUD;
    public EndOfYearUI endOfYearUI;
    public GameObject editorUI, gameUI;
    public SaveLoadMenu loader;
    public ScoutUI scoutUI;
    public PauseUI pauseUI;

    [Space(10)]
    [Header("Game Settings")]
    public string mapToLoadOnPlay;
    

    // Use this for initialization
    void Start () {
        playerResources.gameManager = this;
        resourcesHUD.gameManager = this;
        pointsAction.gameManager = this;
        workers.gameManager = this;
        globalObjectives.gameManager = this;
        upgradeManager.gameManager = this;
        rpManager.gameManager = this;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.Show();
        }
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
        scoutUI.gameObject.SetActive(false);
    }

    //Ressource Getters
    public int GetCurrentPointsAction()
    {
        return pointsAction.Points;
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

    public void AddPlayerResources(ResourceType r, int amount)
    {
        playerResources.AddResources(r, amount);
        if(r == ResourceType.Workers)
        {
            workers.CreateNewWorkers(amount);
        }
    }

    public void RemovePlayerRessources(ResourceType r, int amount)
    {
        playerResources.RemoveResources(r, amount);
    }

    //End of year
    public void EndOfYear()
    {
        resourcesHUD.gameObject.SetActive(false);
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

        }
        else /*if (GameLoader.LoadMode == LoadMode.Edit)*/ //For now we presume that not play is "edit"
        {
            gameController.SetEditMode(true);
            gameUI.SetActive(false);
        }
    }

    IEnumerator LoadGame()
    {
        loader.Load(Path.Combine(Application.dataPath, "StreamingAssets", loader.saveFolder, mapToLoadOnPlay + ".map"));
        yield return new WaitForEndOfFrame();
        rpManager.AddDangersOnResourcePoints();
    }

    
}
