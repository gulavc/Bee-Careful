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
        int[] upgradeCost;
        List<string> prereqs = new List<string>();
        Upgrade newUpgrade;
        string upgradeName;

        //UPGRADES START HERE

        //Upgrade 1
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 20; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ScoutVisionUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 2
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 30; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutVisionUpgrade1");

        //Set name
        upgradeName = "ScoutVisionUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 3
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 60; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutVisionUpgrade2");

        //Set name
        upgradeName = "ScoutVisionUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 4
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ScoutFlyUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade( prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 5
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutFlyUpgrade1");

        //Set name
        upgradeName = "ScoutFlyUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 6
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutFlyUpgrade2");

        //Set name
        upgradeName = "ScoutFlyUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 7
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ScoutMoveUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 8
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutMoveUpgrade1");

        //Set name
        upgradeName = "ScoutMoveUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 9
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutMoveUpgrade2");

        //Set name
        upgradeName = "ScoutMoveUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

    }

    //Assumes that the upgradeAvailable check has already been done. 
    //Returns true if the upgrade has been successfully applied, false if the name supplied does not exist 
    //Or if not enough resources are available
    public bool ApplyUpgrade(string name)
    {

        if (upgrades.ContainsKey(name))
        {
            
            if( !upgrades[name].upgradeUnlocked &&
                IsUpgradeAvailable(name) &&
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
                    case "ScoutVisionUpgrade1":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach(HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }                        
                        break;
                    /*case "ScoutVisionUpgrade2":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutVisionUpgrade3":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutFlyUpgrade1":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }                            
                        break;
                    case "ScoutFlyUpgrade2":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutFlyUpgrade3":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutMoveUpgrade1":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutMoveUpgrade2":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;
                    case "ScoutMoveUpgrade3":
                        scouts = FindObjectsOfType<HexUnit>();
                        foreach (HexUnit h in scouts)
                        {
                            h.ApplyUpgrade(name);
                        }
                        break;*/
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

    public bool IsUpgradeBuyable(string name)
    {
        if (upgrades.ContainsKey(name))
        {

            if (gameManager.GetRessourceCount(ResourceType.Nectar) >= upgrades[name].upgradeCost[0] &&
                gameManager.GetRessourceCount(ResourceType.Water) >= upgrades[name].upgradeCost[1] &&
                gameManager.GetRessourceCount(ResourceType.Resin) >= upgrades[name].upgradeCost[2] &&
                gameManager.GetRessourceCount(ResourceType.Pollen) >= upgrades[name].upgradeCost[3])
            {
                return true;
            }
        }

        return false;
    }

    //Methods to link with the ui
    public void TryApplyUpgrade(string s)
    {
        bool applied = ApplyUpgrade(s);
        
    }



}
