using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    //Static variables
    public static bool waspsTutorialDone = false;
    public static bool pesticideTutorialDone = false;
    public static bool earlyGameTutorial = true;
    public static bool gatherEnabled = false;
    public static bool hiveEnabled = false;

    [HideInInspector]
    public GameManager gameManager;
    public HexGrid grid;

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

    private List<GameObject> allTutorials;

    

    //Beginning Tutorial
    public void ShowStartUpTutorial()
    {
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
        //StartCoroutine(WaitForGather());
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



    //Show any tutorial
    private void ShowTutorial(GameObject toShow)
    {
        StopAllCoroutines();
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
        unit.Location.EnableHighlight(Color.red);
        while (!controller.GetSelectedUnit)
        {
            yield return new WaitForEndOfFrame();
        }

        HexCell target = grid.GetCell(new HexCoordinates(19, 43));
        HexMapCamera.MoveTo(target);

        while(unit.Location != target)
        {
            target.EnableHighlight(Color.red);
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
            target.EnableHighlight(Color.red);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while(gameManager.GetRessourceCount(ResourceType.Pollen) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gatherEnabled = false;

        //Go to RP 2
        target = grid.GetCell(new HexCoordinates(26, 42));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(Color.red);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;
        while (gameManager.GetRessourceCount(ResourceType.Nectar) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gatherEnabled = false;

        //Go to RP 3
        target = grid.GetCell(new HexCoordinates(30, 38));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(Color.red);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while (gameManager.GetRessourceCount(ResourceType.Resin) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gatherEnabled = false;

        //Go to RP 4
        target = grid.GetCell(new HexCoordinates(34, 39));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(Color.red);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        while (gameManager.GetRessourceCount(ResourceType.Water) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gatherEnabled = false;

        //End

        ShowUITutorial();

        //TEMPORARY REMOVE THIS PLEASE OH GOD WHY IS IT STILL HERE
        gatherEnabled = true;
        earlyGameTutorial = false;

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
    }

}
