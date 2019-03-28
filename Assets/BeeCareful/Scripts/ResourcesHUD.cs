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
    public Image seasonTimer;
    public Text pollenText;
    public Text nectarText;
    public Text waterText;
    public Text resinText;
    public Text workerText;

	// Use this for initialization
	void Start () {

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
        UpdateSeasonTimer();
    }

    void UpdateSeasonTimer()
    {
        
        seasonTimer.fillAmount = (float)gameManager.GetCurrentPointsAction() / (float)gameManager.GetPointsActionMax();
    }
}
