using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHUD : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    

    //Hud Elements
    public Slider pollenSlider;
    public Slider waterSlider;
    public Slider nectarSlider;
    public Slider resinSlider;
    public Text workersText;
    public Text pointActionText;
    public Text pollenText;
    public Text nectarText;
    public Text waterText;
    public Text resinText;

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
                waterText.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
                break;
            case ResourceType.Pollen:
                pollenSlider.value = gameManager.GetRessourceCount(ResourceType.Pollen);
                pollenText.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);
                break;
            case ResourceType.Nectar:
                nectarSlider.value = gameManager.GetRessourceCount(ResourceType.Nectar);
                nectarText.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
                break;
            case ResourceType.Resin:
                resinSlider.value = gameManager.GetRessourceCount(ResourceType.Resin);
                resinText.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
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
        pollenText.text = gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen);
        nectarSlider.value = gameManager.GetRessourceCount(ResourceType.Nectar);
        nectarText.text = gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar);
        waterSlider.value = gameManager.GetRessourceCount(ResourceType.Water);
        waterText.text = gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water);
        resinSlider.value = gameManager.GetRessourceCount(ResourceType.Resin);
        resinText.text = gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin);
        workersText.text = "Workforce: " + gameManager.GetRessourceCount(ResourceType.Workers);
        pointActionText.text = "Beenergy: " + gameManager.GetCurrentPointsAction();
    }
}
