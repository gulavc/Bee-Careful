using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHUD : MonoBehaviour {

    public PlayerResources ressources;
    public Slider pollenSlider;
    public Slider waterSlider;
    public Slider nectarSlider;
    public Slider resinSlider;
    public Slider damageSlider;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        pollenSlider.value = ressources.GetCurrentResource(ResourceType.Pollen);
        //Debug.Log("Pollen " + ressources.GetCurrentResource(ResourceType.Pollen) + "  /  " + pollenSlider.value);
        nectarSlider.value = ressources.GetCurrentResource(ResourceType.Nectar);
        waterSlider.value = ressources.GetCurrentResource(ResourceType.Water);
        resinSlider.value = ressources.GetCurrentResource(ResourceType.Resin);
    }
}
