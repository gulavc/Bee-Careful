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
    public GlowPulse UIFocus;

    public HexGrid grid;

    public GameObject hiveButton;

    public GameObject[] seasonTimers;

    public GameObject gatherButton;

    public GameObject workersHUD;

    public GameObject pollenHUD;
    public GameObject nectarHUD;    
    public GameObject resinHUD;    
    public GameObject waterHUD;    

    [Header("Tutorials references")]
    public GameObject startUpTutorial;
    public GameObject cameraSuccessTutorial;
    public GameObject selectTutorial;
    public GameObject moveToTutorial1;
    public GameObject moveToTutorial2;
    public GameObject gatherTutorial1;
    public GameObject gatherTutorial2;
    public GameObject workersTutorial;
    public GameObject gatherTutorial3;
    public GameObject pollenHUDTutorial;
    public GameObject gatherTutorial4;
    public GameObject gatherTutorial5;
    public GameObject nectarHUDTutorial;
    public GameObject gatherTutorial6;
    public GameObject resinHUDTutorial;
    public GameObject gatherTutorial7;
    public GameObject waterHUDTutorial;


    public GameObject mountainTutorial;
    public GameObject waspsTutorial;
    public GameObject pesticideTutorial;
    public GameObject tooFarTutorial;

    private List<GameObject> allTutorials;
    HexGameUI controller; 
    HexUnit unit;


    //Beginning Tutorial
    public void ShowStartUpTutorial()
    {
        TutorialsSetUp();        

        //Start up tuto
        ShowTutorial(startUpTutorial);
        gameManager.RemovePlayerControls();
        StartCoroutine(WaitForCameraMove());
    }

    //Here, we wait for the player to Move the Camera with all the controls
    IEnumerator WaitForCameraMove()
    {
        bool translate = false;
        bool rotate = false;
        bool zoom = false;

        while (!(translate && rotate && zoom))
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                translate = true;
            }

            if (Input.GetAxis("Rotation") != 0)
            {
                rotate = true;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                zoom = true;
            }

            yield return new WaitForEndOfFrame();

        }

        gameManager.RestorePlayerControls();
        ShowCameraSuccessTutorial();
    }


    //Here we congratulate the player and let him know what his next objective will be
    public void ShowCameraSuccessTutorial()
    {
        ShowTutorial(cameraSuccessTutorial);
        StartCoroutine(WaitForCameraSuccessEnd());
    }

    IEnumerator WaitForCameraSuccessEnd()
    {
        while (cameraSuccessTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        ShowSelectTutorial();
    }

    //Here we move the camera to the bee, and wait for him to select the bee
    public void ShowSelectTutorial()
    {
        ShowTutorial(selectTutorial);
        StartCoroutine(WaitForSelect());
    }

    IEnumerator WaitForSelect()
    {        
        HexMapCamera.MoveTo(unit.Location);

        while (!controller.GetSelectedUnit)
        {
            unit.Location.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        ShowMoveToTutorial1();
    }

    //Here we congratulate him and tell him what he'll have to do next
    public void ShowMoveToTutorial1()
    {
        ShowTutorial(moveToTutorial1);
        StartCoroutine(WaitForMoveTo1());
    }

    IEnumerator WaitForMoveTo1()
    {
        while (moveToTutorial1.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        ShowMoveToTutorial2();
    }

    //Here we move the camera to the first waypoint and wait until he moves there
    public void ShowMoveToTutorial2()
    {
        ShowTutorial(moveToTutorial2);
        StartCoroutine(WaitForMoveTo2());
    }

    IEnumerator WaitForMoveTo2()
    {
        gameManager.RestorePlayerControls();

        HexCell target = grid.GetCell(new HexCoordinates(19, 43));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();

        ShowGatherTutorial1();
    }

    //Here we congratulate and tell him he'll have to gather next
    public void ShowGatherTutorial1()
    {
        ShowTutorial(gatherTutorial1);
        StartCoroutine(WaitForGather1());
    }

    IEnumerator WaitForGather1()
    {
        while (gatherTutorial1.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        ShowGatherTutorial2();
    }

    //Here we show the next waitpoint and wait until he reaches the 1st resource point
    public void ShowGatherTutorial2()
    {
        ShowTutorial(gatherTutorial2);
        StartCoroutine(WaitForGather2());
    }

    IEnumerator WaitForGather2()
    {
        gameManager.RestorePlayerControls();

        HexCell target = grid.GetCell(new HexCoordinates(18, 45));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();

        ShowWorkersTutorial();
    }

    //Here we congratulate, teach about workers
    public void ShowWorkersTutorial()
    {
        ShowTutorial(workersTutorial);
        StartCoroutine(WaitForWorkersRead());
    }

    IEnumerator WaitForWorkersRead()
    {

        workersHUD.SetActive(true);
                
        UIFocus.Show();
        UIFocus.MoveFocus(workersHUD.transform.position);
        UIFocus.baseScale = 2f;

        while (workersTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();

        ShowGatherTutorial3();
    }

    //Here we teach about gather button + wait until he clicks it
    public void ShowGatherTutorial3()
    {
        ShowTutorial(gatherTutorial3);
        StartCoroutine(WaitForGather3());
    }
    

    IEnumerator WaitForGather3()
    {
        
        UIFocus.Show();
        UIFocus.MoveFocus(gatherButton.transform.position);
        UIFocus.baseScale = 1f;

        gatherEnabled = true;
        if (controller.GetSelectedUnit)
        {
            controller.SetCurrentCell(unit.Location);
            controller.ShowScoutUI();
        }
        
        while (gameManager.GetRessourceCount(ResourceType.Pollen) == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        
        gameManager.HideScoutUI();
        gatherEnabled = false;

        UIFocus.Hide();
        ShowResourceHUDTutorial();
    }

    //Here we show the 1st resource on the HUD and explain it, then talk about finding more resources
    public void ShowResourceHUDTutorial()
    {
        ShowTutorial(pollenHUDTutorial);
        StartCoroutine(WaitForPollenHUD());
        
    }

    IEnumerator WaitForPollenHUD()
    {
        pollenHUD.SetActive(true);

        UIFocus.Show();
        UIFocus.MoveFocus(pollenHUD.transform.position);
        UIFocus.baseScale = 1.1f;

        while (pollenHUDTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();

        ShowGatherTutorial4();
    }
    

    //Here we show the waitpoint to the 2nd resource, and wait until the player gets there
    public void ShowGatherTutorial4()
    {
        ShowTutorial(gatherTutorial4);
        StartCoroutine(WaitForGather4());
    }

    IEnumerator WaitForGather4()
    {
        gameManager.RestorePlayerControls();
        HexCell target = grid.GetCell(new HexCoordinates(26, 42));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();

        ShowGatherTutorial5();
    }

    //Here we congratulate him on finding resource and wait until he gathers
    public void ShowGatherTutorial5()
    {
        ShowTutorial(gatherTutorial5);
        StartCoroutine(WaitForGather5());
    }

    IEnumerator WaitForGather5()
    {
        UIFocus.Show();
        UIFocus.MoveFocus(gatherButton.transform.position);
        UIFocus.baseScale = 1f;

        gatherEnabled = true;
        if (controller.GetSelectedUnit)
        {
            controller.SetCurrentCell(unit.Location);
            controller.ShowScoutUI();
        }

        while (gameManager.GetRessourceCount(ResourceType.Nectar) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.HideScoutUI();
        gatherEnabled = false;

        UIFocus.Hide();
        ShowNectarHUDTutorial();
    }

    //Here we say good job and tell him we found more resources nearby, go and collect them
    public void ShowNectarHUDTutorial()
    {
        ShowTutorial(nectarHUDTutorial);
        StartCoroutine(WaitForNectarHUD());
    }

    IEnumerator WaitForNectarHUD()
    {
        nectarHUD.SetActive(true);

        UIFocus.Show();
        UIFocus.MoveFocus(nectarHUD.transform.position);
        UIFocus.baseScale = 1.1f;

        while (nectarHUDTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();

        ShowGatherTutorial6();
    }

    //Here we show the waypoint to the 3rd resource and wait until move + gather
    public void ShowGatherTutorial6()
    {
        ShowTutorial(gatherTutorial6);
        StartCoroutine(WaitForGather6());
    }

    IEnumerator WaitForGather6()
    {
        HexCell target = grid.GetCell(new HexCoordinates(30, 38));
        HexMapCamera.MoveTo(target);

        gameManager.RestorePlayerControls();

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        UIFocus.Show();
        UIFocus.MoveFocus(gatherButton.transform.position);
        UIFocus.baseScale = 1f;

        while (gameManager.GetRessourceCount(ResourceType.Resin) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();
        gameManager.HideScoutUI();
        gatherEnabled = false;

        ShowResinHUDTutorial();
    }

    //Here we congratulate + say one more resource left
    public void ShowResinHUDTutorial()
    {
        ShowTutorial(resinHUDTutorial);
        StartCoroutine(WaitForResinHUD());
    }

    IEnumerator WaitForResinHUD()
    {
        resinHUD.SetActive(true);

        UIFocus.Show();
        UIFocus.MoveFocus(resinHUD.transform.position);
        UIFocus.baseScale = 1.1f;

        while (resinHUDTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();

        ShowGatherTutorial7();
    }

    //Here we show the waypoint & wait for move + gather
    public void ShowGatherTutorial7()
    {
        ShowTutorial(gatherTutorial7);
        StartCoroutine(WaitForGather7());
    }

    IEnumerator WaitForGather7()
    {

        gameManager.RestorePlayerControls();

        HexCell target = grid.GetCell(new HexCoordinates(34, 39));
        HexMapCamera.MoveTo(target);

        while (unit.Location != target)
        {
            target.EnableHighlight(goalHighlightColor);
            yield return new WaitForEndOfFrame();
        }

        gameManager.RemovePlayerControls();
        gatherEnabled = true;

        UIFocus.Show();
        UIFocus.MoveFocus(gatherButton.transform.position);
        UIFocus.baseScale = 1f;

        while (gameManager.GetRessourceCount(ResourceType.Water) == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        gameManager.RestorePlayerControls();
        gameManager.HideScoutUI();

        UIFocus.Hide();
        gatherEnabled = false;

        ShowWaterHUDTutorial();
    }

    //Here we congratulate + talk about full HUD & what to do with resources
    public void ShowWaterHUDTutorial()
    {
        ShowTutorial(waterHUDTutorial);
        StartCoroutine(WaitForWaterHUD());        
    }

    IEnumerator WaitForWaterHUD()
    {
        waterHUD.SetActive(true);

        UIFocus.Show();
        UIFocus.MoveFocus(waterHUD.transform.position);
        UIFocus.baseScale = 1.1f;

        while (waterHUDTutorial.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }

        UIFocus.Hide();

        //Shownext();
        TutorialsTearDown();
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
    

   

    //Set up and Tear down

    private void TutorialsSetUp()
    {
        //Set up Tutorials
        waspsTutorialDone = false;
        pesticideTutorialDone = false;
        earlyGameTutorial = true;
        gatherEnabled = false;
        hiveEnabled = false;

        //Element References
        controller = FindObjectOfType<HexGameUI>();
        unit = FindObjectOfType<HexUnit>();

        //Hide parts of UI
        hiveButton.SetActive(false);
        workersHUD.SetActive(false);
        pollenHUD.SetActive(false);
        nectarHUD.SetActive(false);
        resinHUD.SetActive(false);
        waterHUD.SetActive(false);
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
        workersHUD.SetActive(true);
        pollenHUD.SetActive(true);
        nectarHUD.SetActive(true);
        resinHUD.SetActive(true);
        waterHUD.SetActive(true);
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
        allTutorials.Add(cameraSuccessTutorial);
        allTutorials.Add(selectTutorial);
        allTutorials.Add(moveToTutorial1);
        allTutorials.Add(moveToTutorial2);
        allTutorials.Add(gatherTutorial1);
        allTutorials.Add(gatherTutorial2);
        allTutorials.Add(workersTutorial);
        allTutorials.Add(gatherTutorial3);
        allTutorials.Add(pollenHUDTutorial);
        allTutorials.Add(gatherTutorial4);
        allTutorials.Add(gatherTutorial5);
        allTutorials.Add(nectarHUDTutorial);
        allTutorials.Add(gatherTutorial6);
        allTutorials.Add(resinHUDTutorial);
        allTutorials.Add(gatherTutorial7);
        allTutorials.Add(waterHUDTutorial);
        

        allTutorials.Add(mountainTutorial);
        allTutorials.Add(waspsTutorial);
        allTutorials.Add(pesticideTutorial);
        allTutorials.Add(tooFarTutorial);
    }

}
