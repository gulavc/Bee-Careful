using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHUD : MonoBehaviour {

    private PlayerResources resources;
    private GlobalObjectives globalObjectives;

    public Slider pollenSlider;
    public Slider waterSlider;
    public Slider nectarSlider;
    public Slider resinSlider;
    public Slider damageSlider;

    public Text nectarObjectiveText;
    public Text waterObjectiveText;
    public Text resinObjectiveText;
    public Text pollenObjectiveText;

	// Use this for initialization
	void Start () {

        resources = GameObject.FindObjectOfType<PlayerResources>();
        globalObjectives = GameObject.FindObjectOfType<GlobalObjectives>();

    }
	
	// Update is called once per frame
	void Update () {
        pollenSlider.value = resources.GetCurrentResource(ResourceType.Pollen);
        nectarSlider.value = resources.GetCurrentResource(ResourceType.Nectar);
        waterSlider.value = resources.GetCurrentResource(ResourceType.Water);
        resinSlider.value = resources.GetCurrentResource(ResourceType.Resin);

        nectarObjectiveText.text = globalObjectives.VerifyObjective(ResourceType.Nectar) ? "Complete!" : resources.GetCurrentResource(ResourceType.Nectar) + " / " + globalObjectives.GetObjective(ResourceType.Nectar);
        waterObjectiveText.text = globalObjectives.VerifyObjective(ResourceType.Water) ? "Complete!" : resources.GetCurrentResource(ResourceType.Water) + " / " + globalObjectives.GetObjective(ResourceType.Water);
        resinObjectiveText.text = globalObjectives.VerifyObjective(ResourceType.Resin) ? "Complete!" : resources.GetCurrentResource(ResourceType.Resin) + " / " + globalObjectives.GetObjective(ResourceType.Resin);
        pollenObjectiveText.text = globalObjectives.VerifyObjective(ResourceType.Pollen) ? "Complete!" : resources.GetCurrentResource(ResourceType.Pollen) + " / " + globalObjectives.GetObjective(ResourceType.Pollen);

    }
}
