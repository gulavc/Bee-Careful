using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHUD : MonoBehaviour {

    public GameManager gameManager;
    

    //Hud Elements
    public Slider pollenSlider;
    public Slider waterSlider;
    public Slider nectarSlider;
    public Slider resinSlider;
    public Slider damageSlider;
    public Text workersText;
    public Text pointActionText;

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
                waterSlider.value = gameManager.GetRessourceCount(ResourceType.Water);
                break;
            case ResourceType.Pollen:
                pollenSlider.value = gameManager.GetRessourceCount(ResourceType.Pollen);
                break;
            case ResourceType.Nectar:
                nectarSlider.value = gameManager.GetRessourceCount(ResourceType.Nectar);
                break;
            case ResourceType.Resin:
                resinSlider.value = gameManager.GetRessourceCount(ResourceType.Resin);
                break;
            case ResourceType.Workers:
                workersText.text = "Workers: " + gameManager.GetRessourceCount(ResourceType.Workers);
                break;
            default:
                UpdateHUDAllResources();
                break;

        }
    }

    public void UpdatePointsActionHUD()
    {
        pointActionText.text = "Points d'Action: " + gameManager.GetCurrentPointsAction();
    }

    public void UpdateHUDAllResources()
    {
        pollenSlider.value = gameManager.GetRessourceCount(ResourceType.Pollen);
        nectarSlider.value = gameManager.GetRessourceCount(ResourceType.Nectar);
        waterSlider.value = gameManager.GetRessourceCount(ResourceType.Water);
        resinSlider.value = gameManager.GetRessourceCount(ResourceType.Resin);
        workersText.text = "Workers: " + gameManager.GetRessourceCount(ResourceType.Workers);
        pointActionText.text = "Points d'Action: " + gameManager.GetCurrentPointsAction();
    }
}
