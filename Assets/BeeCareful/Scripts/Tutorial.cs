using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    //Static variables, default state is Tuto done to allow skipping
    public static bool waspsTutorialDone = true;
    public static bool pesticideTutorialDone = true;
    public static bool earlyGameTutorial = false;
    public static bool gatherEnabled = true;
    public static bool hiveEnabled = true;

    [HideInInspector]
    public GameManager gameManager;

    [Header("Tutorial Settings")]
    public Color goalHighlightColor = Color.green;

    [Header("Game elements references")]
    public HexGrid grid;
    public GameObject hiveButton;
    public GameObject[] seasonTimers;
    
    [Header("Tutorials references")]
    public GameObject startUpTutorial;
    public GameObject selectMoveTutorial;
    public GameObject gatherTutorial;
    public GameObject UITutorial;
    public GameObject hiveTutorial;
    public GameObject hiveUITutorial;
    public GameObject spawnTutorial;
    public GameObject upgradeTutorial;
    public GameObject buyUpgradeTutorial;
    public GameObject pointsActionTutorial;

    public GameObject mountainTutorial;
    public GameObject waspsTutorial;
    public GameObject pesticideTutorial;
    public GameObject tooFarTutorial;

    private List<GameObject> allTutorials;

    

    //Beginning Tutorial
    public void ShowStartUpTutorial()
    {
        TutorialsSetUp();        

        //Start up tuto
        ShowTutorial(startUpTutorial);
        gameManager.RemovePlayerControls();
        StartCoroutine(WaitForCameraMove());
    }

    public void ShowSelectMoveTutorial()
    {
        ShowTutorial(selectMoveTutorial);
        StartCoroutine(WaitForSelectAndMove());
    }

    public void ShowGatherTutorial ()
    {
        ShowTutorial(gatherTutorial);
        StartCoroutine(WaitForGather());
    }

    public void ShowUITutorial()
    {
        ShowTutorial(UITutorial);
        StartCoroutine(WaitForMoveToHive());
    }

    public void ShowHiveTutorial()
    {
        ShowTutorial(hiveTutorial);
        //StartCoroutine(WaitForOpenHive());
    }

    //New year messages

    //Generic Tutorials
    public void ShowMountainTutorial()
    {
        ShowTutorial(mountainTutorial);
    }

    public void ShowWaspsTutorial()
    {
        if (!waspsTutorialDone)
        {
            ShowTutorial(waspsTutorial);
            waspsTutorialDone = true;
        }
    }

    public void ShowPesticideTutorial()
    {
        if (!pesticideTutorialDone)
        {
            ShowTutorial(pesticideTutorial);
            pesticideTutorialDone = true;
        }
    }

    public void ShowTooFarTutorial()
    {
        ShowTutorial(tooFarTutorial, false);
    }

    

    //Show any tutorial
    private void ShowTutorial(GameObject toShow, bool stopCoroutines = true)
    {
        if (stopCoroutines)
        {
            StopAllCoroutines();
        }        
        gameManager.RestorePlayerControls();
        foreach(GameObject g in allTutorials)
        {
            g.SetActive(false);
        }

        toShow.SetActive(true);
    }

    //Coroutines for Checking conditions
    IEnumerator WaitForCameraMove()
    {
        bool translate = false;
        bool rotate = false;
        bool zoom = false;

        while(!(translate && rotate && zoom))
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                translate = true;
            }

            if(Input.GetAxis("Rotation") != 0)
            {
                rotate = true;
            }

            if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                zoom = true;
            }

            yield return new WaitForEndOfFrame();

        }

        gameManager.RestorePlayerControls();
        ShowSelectMoveTutorial();
    }

    IEnumerator WaitForSelectAndMove()
    {
        HexGameUI controller = FindObjectOfType<HexGameUI>();
        HexUnit unit = FindObjectOfType<HexUnit>();
        
        while (!controller.GetSelectedUnit)
        {
            unit.Location.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        //ShowTutorial MoveTo
        //Wait for finish reading

        HexCell target = grid.GetCell(new HexCoordinates(19, 43));
        HexMapCamera.MoveTo(target);

        while(unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        ShowGatherTutorial();

    }


    IEnumerator WaitForGather()
    {
        HexUnit unit = FindObjectOfType<HexUnit>();

        //Go to RP 1
        HexCell target = grid.GetCell(new HexCoordinates(18, 45));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while(gameManager.GetRessourceCount(ResourceType.Pollen) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gameManager.HideScoutUI();
        gatherEnabled = false;

        //Go to RP 2
        target = grid.GetCell(new HexCoordinates(26, 42));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;
        while (gameManager.GetRessourceCount(ResourceType.Nectar) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gameManager.HideScoutUI();
        gatherEnabled = false;

        //Go to RP 3
        target = grid.GetCell(new HexCoordinates(30, 38));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while (gameManager.GetRessourceCount(ResourceType.Resin) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gameManager.HideScoutUI();
        gatherEnabled = false;

        //Go to RP 4
        target = grid.GetCell(new HexCoordinates(34, 39));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while (gameManager.GetRessourceCount(ResourceType.Water) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gameManager.HideScoutUI();
        gatherEnabled = false;

        //End
        ShowUITutorial();        

    }

    IEnumerator WaitForMoveToHive()
    {
        HexUnit unit = FindObjectOfType<HexUnit>();

        //Hive
        List<HexCell> targets = new List<HexCell>();
        targets.Add(grid.GetCell(new HexCoordinates(20, 39)));
        targets.Add(grid.GetCell(new HexCoordinates(21, 38)));
        targets.Add(grid.GetCell(new HexCoordinates(22, 38)));
        targets.Add(grid.GetCell(new HexCoordinates(22, 39)));
        targets.Add(grid.GetCell(new HexCoordinates(20, 40)));
        targets.Add(grid.GetCell(new HexCoordinates(21, 40)));

        HexMapCamera.MoveTo(grid.GetCell(new HexCoordinates(21, 39)));

        while (!targets.Contains(unit.Location))
        {            
            foreach(HexCell c in targets)
            {
                c.EnableHighlight(goalHighlightColor);
            }
            yield return new WaitForEndOfFrame();

        }

        foreach (HexCell c in targets)
        {
            c.DisableHighlight();
        }

        ShowHiveTutorial();

        //Temp tear down here
        TutorialsTearDown();
    }


    //Set up and Tear down

    private void TutorialsSetUp()
    {
        //Set up Tutorials
        waspsTutorialDone = false;
        pesticideTutorialDone = false;
        earlyGameTutorial = true;
        gatherEnabled = false;
        hiveEnabled = false;

        //Hide parts of UI
        hiveButton.SetActive(false);
        foreach(GameObject g in seasonTimers)
        {
            g.SetActive(false);
        }

        //here we cheat
        gameManager.SetCurrentPointsAction(int.MaxValue, true);
    }

    private void TutorialsTearDown()
    {
        gatherEnabled = true;
        earlyGameTutorial = false;
        hiveEnabled = true;

        hiveButton.SetActive(true);
        foreach (GameObject g in seasonTimers)
        {
            g.SetActive(true);
        }
        gameManager.SetCurrentPointsAction(gameManager.GetPointsActionMax());
    }

    //This is bad code, shame on you Guillaume
    private void Awake()
    {
        allTutorials = new List<GameObject>();
        allTutorials.Add(startUpTutorial);
        allTutorials.Add(selectMoveTutorial);
        allTutorials.Add(gatherTutorial);
        allTutorials.Add(UITutorial);
        allTutorials.Add(hiveTutorial);
        allTutorials.Add(hiveUITutorial);
        allTutorials.Add(spawnTutorial);
        allTutorials.Add(upgradeTutorial);
        allTutorials.Add(buyUpgradeTutorial);
        allTutorials.Add(pointsActionTutorial);
        allTutorials.Add(mountainTutorial);
        allTutorials.Add(waspsTutorial);
        allTutorials.Add(pesticideTutorial);
        allTutorials.Add(tooFarTutorial);
    }

}
