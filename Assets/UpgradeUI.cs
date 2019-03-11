using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    public Toggle scoutVisionToggle, scoutFlyToggle;

    public UpgradeManager upgradeManager;

	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void TryApplyUpgrade(string upgradeName)
    {
        bool result = upgradeManager.ApplyUpgrade(upgradeName);
        if (result)
        {
            Debug.Log("upgrade yay!");
            switch (upgradeName)
            {
                case "ScoutVisionUpgrade":
                    scoutVisionToggle.interactable = false;
                    break;
                case "ScoutFlyUpgrade":
                    scoutFlyToggle.interactable = false;
                    break;
                default:
                    Debug.LogError("No such upgrade name");
                    break;
            }
            
        }
        else
        {
            switch (upgradeName)
            {
                case "ScoutVisionUpgrade":
                    scoutVisionToggle.isOn = false;
                    break;
                case "ScoutFlyUpgrade":
                    scoutFlyToggle.isOn = false;
                    break;
                default:
                    Debug.LogError("No such upgrade name");
                    break;
            }
        }
    }
}
