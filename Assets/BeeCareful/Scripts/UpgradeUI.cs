using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{

    public Toggle visionMore1, visionMore2, visionMore3, flyOverMountains, flyOverRivers, flyOverCities, moveMore1, moveMore2, moveMore3,
        gatherMore1, gatherMore2, gatherMore3, unfuckWasps1, unfuckWasps2, unfuckWasps3, unfuckPoison1, unfuckPoison2, unfuckPoison3,
        cheapWork1, cheapWork2, cheapWork3, cheapScout1, cheapScout2, cheapScout3, startScout1, startScout2, startScout3, startWork1, startWork2, startWork3;

    public UpgradeManager upgradeManager;

    public Sprite unavailableImage, availableImage, costyImage, buyableImage;


    // Use this for initialization
    void Start()
    {
        UpgradeAvailableCheck();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpgradeAvailableCheck()
    {
        Dictionary<string, Toggle> upgradeList = new Dictionary<string, Toggle>();

        //Scout upgrades list here
        upgradeList.Add("ScoutVisionUpgrade1", visionMore1);
        upgradeList.Add("ScoutVisionUpgrade2", visionMore2);
        upgradeList.Add("ScoutVisionUpgrade3", visionMore3);

        upgradeList.Add("ScoutFlyUpgrade1", flyOverMountains);
        upgradeList.Add("ScoutFlyUpgrade2", flyOverRivers);
        upgradeList.Add("ScoutFlyUpgrade3", flyOverCities);

        upgradeList.Add("ScoutMoveUpgrade1", moveMore1);
        upgradeList.Add("ScoutMoveUpgrade2", moveMore2);
        upgradeList.Add("ScoutMoveUpgrade3", moveMore3);

        //Worker upgrades list here
        upgradeList.Add("GatherMoreUpgrade1", gatherMore1);
        upgradeList.Add("GatherMoreUpgrade2", gatherMore2);
        upgradeList.Add("GatherMoreUpgrade3", gatherMore3);

        upgradeList.Add("ProtectWaspsUpgrade1", unfuckWasps1);
        upgradeList.Add("ProtectWaspsUpgrade2", unfuckWasps2);
        upgradeList.Add("ProtectWaspsUpgrade3", unfuckWasps3);

        upgradeList.Add("ProtectPesticideUpgrade1", unfuckPoison1);
        upgradeList.Add("ProtectPesticideUpgrade2", unfuckPoison2);
        upgradeList.Add("ProtectPesticideUpgrade3", unfuckPoison3);

        //Spawn upgrades list here
        upgradeList.Add("CheapWorkUpgrade1", cheapWork1);
        upgradeList.Add("CheapWorkUpgrade2", cheapWork2);
        upgradeList.Add("CheapWorkUpgrade3", cheapWork3);

        upgradeList.Add("CheapScoutUpgrade1", cheapScout1);
        upgradeList.Add("CheapScoutUpgrade2", cheapScout2);
        upgradeList.Add("CheapScoutUpgrade3", cheapScout3);

        upgradeList.Add("StartScoutUpgrade1", startScout1);
        upgradeList.Add("StartScoutUpgrade2", startScout2);
        upgradeList.Add("StartScoutUpgrade3", startScout3);

        upgradeList.Add("StartWorkUpgrade1", startWork1);
        upgradeList.Add("StartWorkUpgrade2", startWork2);
        upgradeList.Add("StartWorkUpgrade3", startWork3);


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
                    flyOverCities.interactable = false;
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

                case "GatherMoreUpgrade1":
                    gatherMore1.interactable = false;
                    break;
                case "GatherMoreUpgrade2":
                    gatherMore2.interactable = false;
                    break;
                case "GatherMoreUpgrade3":
                    gatherMore3.interactable = false;
                    break;

                case "ProtectWaspsUpgrade1":
                    unfuckWasps1.interactable = false;
                    break;
                case "ProtectWaspsUpgrade2":
                    unfuckWasps2.interactable = false;
                    break;
                case "ProtectWaspsUpgrade3":
                    unfuckWasps3.interactable = false;
                    break;

                case "ProtectPesticideUpgrade1":
                    unfuckPoison1.interactable = false;
                    break;
                case "ProtectPesticideUpgrade2":
                    unfuckPoison2.interactable = false;
                    break;
                case "ProtectPesticideUpgrade3":
                    unfuckPoison3.interactable = false;
                    break;

                case "CheapWorkUpgrade1":
                    cheapWork1.interactable = false;
                    break;
                case "CheapWorkUpgrade2":
                    cheapWork2.interactable = false;
                    break;
                case "CheapWorkUpgrade3":
                    cheapWork3.interactable = false;
                    break;

                case "CheapScoutUpgrade1":
                    cheapScout1.interactable = false;
                    break;
                case "CheapScoutUpgrade2":
                    cheapScout2.interactable = false;
                    break;
                case "CheapScoutUpgrade3":
                    cheapScout3.interactable = false;
                    break;

                case "StartScoutUpgrade1":
                    startScout1.interactable = false;
                    break;
                case "StartScoutUpgrade2":
                    startScout2.interactable = false;
                    break;
                case "StartScoutUpgrade3":
                    startScout3.interactable = false;
                    break;

                case "StartWorkUpgrade1":
                    startWork1.interactable = false;
                    break;
                case "StartWorkUpgrade2":
                    startWork2.interactable = false;
                    break;
                case "StartWorkUpgrade3":
                    startWork3.interactable = false;
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
                    flyOverCities.isOn = false;
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
                case "GatherMoreUpgrade1":
                    gatherMore1.isOn = false;
                    break;
                case "GatherMoreUpgrade2":
                    gatherMore2.isOn = false;
                    break;
                case "GatherMoreUpgrade3":
                    gatherMore3.isOn = false;
                    break;

                case "ProtectWaspsUpgrade1":
                    unfuckWasps1.isOn = false;
                    break;
                case "ProtectWaspsUpgrade2":
                    unfuckWasps2.isOn = false;
                    break;
                case "ProtectWaspsUpgrade3":
                    unfuckWasps3.isOn = false;
                    break;

                case "ProtectPesticideUpgrade1":
                    unfuckPoison1.isOn = false;
                    break;
                case "ProtectPesticideUpgrade2":
                    unfuckPoison2.isOn = false;
                    break;
                case "ProtectPesticideUpgrade3":
                    unfuckPoison3.isOn = false;
                    break;

                case "CheapWorkUpgrade1":
                    cheapWork1.isOn = false;
                    break;
                case "CheapWorkUpgrade2":
                    cheapWork2.isOn = false;
                    break;
                case "CheapWorkUpgrade3":
                    cheapWork3.isOn = false;
                    break;

                case "CheapScoutUpgrade1":
                    cheapScout1.isOn = false;
                    break;
                case "CheapScoutUpgrade2":
                    cheapScout2.isOn = false;
                    break;
                case "CheapScoutUpgrade3":
                    cheapScout3.isOn = false;
                    break;

                case "StartScoutUpgrade1":
                    startScout1.isOn = false;
                    break;
                case "StartScoutUpgrade2":
                    startScout2.isOn = false;
                    break;
                case "StartScoutUpgrade3":
                    startScout3.isOn = false;
                    break;

                case "StartWorkUpgrade1":
                    startWork1.isOn = false;
                    break;
                case "StartWorkUpgrade2":
                    startWork2.isOn = false;
                    break;
                case "StartWorkUpgrade3":
                    startWork3.isOn = false;
                    break;

                default:
                    Debug.LogError("No such upgrade name");
                    break;
            }
        }

        UpgradeAvailableCheck();
    }
}
