using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    public Toggle visionMore1, visionMore2, visionMore3, flyOverMountains, flyOverRivers, flyOverBeasts, moveMore1, moveMore2, moveMore3;

    public UpgradeManager upgradeManager;

    public Sprite unavailableImage, availableImage, costyImage, buyableImage;


	// Use this for initialization
	void Start () {
        UpgradeAvailableCheck();



    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpgradeAvailableCheck()
    {
        Dictionary<string, Toggle> upgradeList = new Dictionary<string, Toggle>();
        upgradeList.Add("ScoutVisionUpgrade1", visionMore1);
        upgradeList.Add("ScoutVisionUpgrade2", visionMore2);
        upgradeList.Add("ScoutVisionUpgrade3", visionMore3);


        upgradeList.Add("ScoutFlyUpgrade1", flyOverMountains);
        upgradeList.Add("ScoutFlyUpgrade2", flyOverRivers);
        upgradeList.Add("ScoutFlyUpgrade3", flyOverBeasts);

        upgradeList.Add("ScoutMoveUpgrade1", moveMore1);
        upgradeList.Add("ScoutMoveUpgrade2", moveMore2);
        upgradeList.Add("ScoutMoveUpgrade3", moveMore3);

        foreach (string upgrade in upgradeList.Keys)
        {
            if (upgradeManager.IsUpgradeBuyable(upgrade) && upgradeManager.IsUpgradeAvailable(upgrade)) // Si t'as du cash p'is y'est disponible
            {
                upgradeList[upgrade].GetComponent<Image>().sprite = availableImage;
                SpriteState st = upgradeList[upgrade].spriteState;
                st.highlightedSprite = buyableImage;
                st.pressedSprite = buyableImage;
                upgradeList[upgrade].spriteState = st;
            }
            else if (upgradeManager.IsUpgradeAvailable(upgrade)) // Si t'es PAUVRE, mais il est disponible
            {
                upgradeList[upgrade].GetComponent<Image>().sprite = availableImage;
                SpriteState st = upgradeList[upgrade].spriteState;
                st.highlightedSprite = costyImage;
                st.pressedSprite = availableImage;
                upgradeList[upgrade].spriteState = st;
            }
            else
            {
                upgradeList[upgrade].GetComponent<Image>().sprite = unavailableImage; // Si y'est PAS DISPONIBLE
                SpriteState st = upgradeList[upgrade].spriteState;
                st.highlightedSprite = unavailableImage;
                st.pressedSprite = unavailableImage;
                upgradeList[upgrade].spriteState = st;
            }
        }    


    }

    public void TryApplyUpgrade(string upgradeName)
    {
        bool result = upgradeManager.ApplyUpgrade(upgradeName);
        if (result)
        {
            Debug.Log("upgrade yay!");
            switch (upgradeName)
            {
                case "ScoutVisionUpgrade1":
                    visionMore1.interactable = false;
                    break;
                case "ScoutVisionUpgrade2":
                    visionMore2.interactable = false;
                    break;
                case "ScoutVisionUpgrade3":
                    visionMore3.interactable = false;
                    break;
                case "ScoutFlyUpgrade1":
                    flyOverMountains.interactable = false;
                    break;
                case "ScoutFlyUpgrade2":
                    flyOverRivers.interactable = false;
                    break;
                case "ScoutFlyUpgrade3":
                    flyOverBeasts.interactable = false;
                    break;
                case "ScoutMoveUpgrade1":
                    moveMore1.interactable = false;
                    break;
                case "ScoutMoveUpgrade2":
                    moveMore2.interactable = false;
                    break;
                case "ScoutMoveUpgrade3":
                    moveMore3.interactable = false;
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
                case "ScoutVisionUpgrade1":
                    visionMore1.isOn = false;
                    break;
                case "ScoutVisionUpgrade2":
                    visionMore2.isOn = false;
                    break;
                case "ScoutVisionUpgrade3":
                    visionMore3.isOn = false;
                    break;
                case "ScoutFlyUpgrade1":
                    flyOverMountains.isOn = false;
                    break;
                case "ScoutFlyUpgrade2":
                    flyOverRivers.isOn = false;
                    break;
                case "ScoutFlyUpgrade3":
                    flyOverBeasts.isOn = false;
                    break;
                case "ScoutMoveUpgrade1":
                    moveMore1.isOn = false;
                    break;
                case "ScoutMoveUpgrade2":
                    moveMore2.isOn = false;
                    break;
                case "ScoutMoveUpgrade3":
                    moveMore3.isOn = false;
                    break;

                default:
                    Debug.LogError("No such upgrade name");
                    break;
            }
        }

        UpgradeAvailableCheck();
    }
}
