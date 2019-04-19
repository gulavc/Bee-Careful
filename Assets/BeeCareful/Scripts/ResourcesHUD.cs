using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHUD : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    

    //Hud Elements
    public Image pollenHex;
    public Image waterHex;
    public Image nectarHex;
    public Image resinHex;
    public Image workerHex;    
    public Text pollenText;
    public Text nectarText;
    public Text waterText;
    public Text resinText;
    public Text workerText;

    [Header("Season Timers")]
    public Image seasonTimerSpring;
    public Image seasonTimerRedBarSpring;
    public Image seasonTimerSummer;
    public Image seasonTimerRedBarSummer;
    public Image seasonTimerFall;
    public Image seasonTimerRedBarFall;

    [Header("Season Buttons")]
    public Button hiveButtonSpring;
    public Button hiveButtonSummer;
    public Button hiveButtonFall;

    //private Image seasonTimer;

    // Use this for initialization
    void OnEnable () {
        //seasonTimer = seasonTimerSpring;
        seasonTimerSpring.gameObject.SetActive(true);
        seasonTimerSummer.gameObject.SetActive(true);
        seasonTimerFall.gameObject.SetActive(true);
        seasonTimerSpring.fillAmount = 1f;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void UpdateHUD(ResourceType r)
    {
        switch (r)
        {
            case ResourceType.Water:
                waterHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Water) / 200f; //VALEUR TEMPORAIRE À CHANGER
                waterText.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
                break;
            case ResourceType.Pollen:
                pollenHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Pollen) / 200f; //VALEUR TEMPORAIRE À CHANGER
                pollenText.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);
                break;
            case ResourceType.Nectar:
                nectarHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Nectar) / 200f; // VALEUR TEMPORAIRE À CHANGER
                nectarText.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
                break;
            case ResourceType.Resin:
                resinHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Resin) / 200f; // VALEUR TEMPO À CHANGER
                resinText.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
                break;
            case ResourceType.Workers:
                workerHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Workers) / 200f; // IDEM
                workerText.text = gameManager.GetRessourceCount(ResourceType.Workers) + "";
                break;
            default:
                UpdateHUDAllResources();
                break;

        }
    }

    public void UpdatePointsActionHUD()
    {
        UpdateSeasonTimer();
    }

    public void UpdateHUDAllResources()
    {
        pollenHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Pollen) / 200f; //VALEUR TEMPORAIRE À CHANGER
        pollenText.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);
        nectarHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Nectar) / 200f; // VALEUR À CHANGER
        nectarText.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
        waterHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Water) / 200f; // VALEUR TEMPORAIRE À CHANGER
        waterText.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
        resinHex.fillAmount = gameManager.GetRessourceCount(ResourceType.Resin) / 200f; // IDEM
        resinText.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
        workerHex.fillAmount = (float)gameManager.GetRessourceCount(ResourceType.Workers) / 200f; //IDEM
        workerText.text = gameManager.GetRessourceCount(ResourceType.Workers) + "";
        //workerText.text = gameManager.GetRessourceCount(ResourceType.Workers) + "";
        UpdateSeasonTimer();
    }

    void UpdateSeasonTimer()
    {
        int currentTime = gameManager.GetCurrentPointsAction();
        int maxTime = gameManager.GetPointsActionMax();
        //seasonTimer.gameObject.SetActive(false);
        hiveButtonFall.gameObject.SetActive(false);
        hiveButtonSummer.gameObject.SetActive(false);
        hiveButtonSpring.gameObject.SetActive(false);
        if(currentTime > maxTime * (2 / 3f))
        {
            hiveButtonSpring.gameObject.SetActive(true);
        }
        else if (currentTime > maxTime * (1 / 3f))
        {
            hiveButtonSummer.gameObject.SetActive(true);
        }
        else
        {
            hiveButtonFall.gameObject.SetActive(true);
        }
        //seasonTimer.gameObject.SetActive(true);
        //seasonTimer.fillAmount = (float)currentTime / (float)maxTime;
        float third = maxTime / 3f;
        /*seasonTimer.fillAmount = (currentTime % third) / third;*/

        seasonTimerRedBarSpring.fillAmount = (currentTime - (third * 2)) / third;
        seasonTimerRedBarSummer.fillAmount = (currentTime - third) / third;
        seasonTimerRedBarFall.fillAmount = currentTime / third;

    }


    public void PreviewSeasonTimer(int distance)
    {
        int currentTime = gameManager.GetCurrentPointsAction() - distance;
        int maxTime = gameManager.GetPointsActionMax();
        float third = maxTime / 3f;
        seasonTimerSpring.fillAmount = (currentTime - (third * 2)) / third;
        seasonTimerSummer.fillAmount = (currentTime - third) / third;
        seasonTimerFall.fillAmount = currentTime / third;
    }

    public void ResetSeasonTimerPreview()
    {
        seasonTimerSpring.fillAmount = seasonTimerRedBarSpring.fillAmount;
        seasonTimerSummer.fillAmount = seasonTimerRedBarSummer.fillAmount;
        seasonTimerFall.fillAmount = seasonTimerRedBarFall.fillAmount;
    }
}
