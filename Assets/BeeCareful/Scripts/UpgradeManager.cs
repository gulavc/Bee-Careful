using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    Dictionary<string, Upgrade> upgrades;
    [HideInInspector] public GameManager gameManager;

    HexUnit[] scouts;

    void Start()
    {
        //Initial declarations, don't touch this section
        upgrades = new Dictionary<string, Upgrade>();
        int[] upgradeCost = new int[4];
        List<string> prereqs = new List<string>();
        Upgrade newUpgrade;
        string upgradeName;

        //UPGRADES START HERE

        //Upgrade 1
        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ScoutVisionUpgrade";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 2
        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Add("ScoutVisionUpgrade");

        //Set name
        upgradeName = "ScoutFlyUpgrade";

        //Don't touch this
        newUpgrade = new Upgrade( prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 3

    }

    //Assumes that the upgradeAvailable check has already been done. 
    //Returns true if the upgrade has been successfully applied, false if the name supplied does not exist 
    //Or if not enough resources are available
    public bool ApplyUpgrade(string name)
    {

        if (upgrades.ContainsKey(name))
        {
            
            if( !upgrades[name].upgradeUnlocked &&
                gameManager.GetRessourceCount(ResourceType.Nectar) >= upgrades[name].upgradeCost[0] &&
                gameManager.GetRessourceCount(ResourceType.Water) >= upgrades[name].upgradeCost[1] &&
                gameManager.GetRessourceCount(ResourceType.Resin) >= upgrades[name].upgradeCost[2] &&
                gameManager.GetRessourceCount(ResourceType.Pollen) >= upgrades[name].upgradeCost[3])
            {
                gameManager.RemovePlayerRessources(ResourceType.Nectar, upgrades[name].upgradeCost[0]);
                gameManager.RemovePlayerRessources(ResourceType.Water, upgrades[name].upgradeCost[1]);
                gameManager.RemovePlayerRessources(ResourceType.Resin, upgrades[name].upgradeCost[2]);
                gameManager.RemovePlayerRessources(ResourceType.Pollen, upgrades[name].upgradeCost[3]);

                switch (name)
                {
                    case "ScoutVisionUpgrade":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach(HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }                        
                        break;
                    case "ScoutFlyUpgrade":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }                            
                        break;
                    default:
                        Debug.LogError("No such upgrade");
                        break;
                }

                upgrades[name].Unlock();
                return true;

            }
            else
            {
                //Not enough resources
                Debug.Log("Not enough resources");
                return false;
            }
            

            
        }
        else
        {
            Debug.LogError("Upgrade: " + name + " does not exist");
            return false;
        }
    }

    //Returns true if all the prerquisites for the specified upgrades have been acquired
    //False if the name supplied does not exist
    //Or if any of the prerequisites are not acquired
    public bool IsUpgradeAvailable(string name)
    {
        if (upgrades.ContainsKey(name))
        {           
            
            foreach (string s in upgrades[name].upgradePrerequisites)
            {
                if (!IsUpgradeAcquired(s))
                {
                    return false;
                }
            }

            return true; ;
        }
        else
        {
            Debug.LogError("Upgrade: " + name + " does not exist");
            return false;
        }

    }

    //Returns true if the upgrade has already been acquired
    //False if not or if the upgrade name doesn't exist
    public bool IsUpgradeAcquired(string name)
    {
        if (upgrades.ContainsKey(name))
        {
            return upgrades[name].upgradeUnlocked;
        }
        else
        {
            Debug.LogError("Upgrade: " + name + " does not exist");
            return false;
        }
    }

    //Methods to link with the ui
    public void TryApplyUpgrade(string s)
    {
        bool applied = ApplyUpgrade(s);
        
    }

    //TO TEST UPGRADES PLEASE REMOVE THIS CODE AT SOME POINT OMG WHY IS IT STILL THERE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string test = "ScoutVisionUpgrade";
            upgradeTest(test);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string test = "ScoutFlyUpgrade";
            upgradeTest(test);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            string test = "ScoutGodModeUpgrade";
            upgradeTest(test);
        }
    }

    void upgradeTest(string name)
    {
        if (IsUpgradeAvailable(name))
        {
            ApplyUpgrade(name);
        }
        else
        {
            Debug.Log("Upgrade not available");
        }
    }

}
