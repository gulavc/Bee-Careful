using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

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
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 15; //Pollen

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
        upgradeCost[3] = 30; //Pollen

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
        upgradeCost[0] = 45; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 45; //Pollen

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
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 30; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ScoutFlyUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 5
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 30; //Eau
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
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 30; //Resine
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
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 0; //Pollen

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
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 20; //Eau
        upgradeCost[2] = 20; //Resine
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
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 40; //Eau
        upgradeCost[2] = 40; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ScoutMoveUpgrade2");

        //Set name
        upgradeName = "ScoutMoveUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 10
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "GatherMoreUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 11
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 15; //Eau
        upgradeCost[2] = 15; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("GatherMoreUpgrade1");

        //Set name
        upgradeName = "GatherMoreUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 12
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 20; //Eau
        upgradeCost[2] = 20; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("GatherMoreUpgrade2");

        //Set name
        upgradeName = "GatherMoreUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 13
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ProtectWaspsUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 14
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 20; //Eau
        upgradeCost[2] = 20; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ProtectWaspsUpgrade1");

        //Set name
        upgradeName = "ProtectWaspsUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 15
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 0; //Nectar
        upgradeCost[1] = 30; //Eau
        upgradeCost[2] = 30; //Resine
        upgradeCost[3] = 0; //Pollen

        //Set prereqs
        prereqs.Add("ProtectWaspsUpgrade2");

        //Set name
        upgradeName = "ProtectWaspsUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 16
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ProtectPesticideUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 17
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 20; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 20; //Pollen

        //Set prereqs
        prereqs.Add("ProtectPesticideUpgrade1");

        //Set name
        upgradeName = "ProtectPesticideUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 18
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 30; //Nectar
        upgradeCost[1] = 0; //Eau
        upgradeCost[2] = 0; //Resine
        upgradeCost[3] = 30; //Pollen

        //Set prereqs
        prereqs.Add("ProtectPesticideUpgrade2");

        //Set name
        upgradeName = "ProtectPesticideUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 19
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 5; //Nectar
        upgradeCost[1] = 5; //Eau
        upgradeCost[2] = 5; //Resine
        upgradeCost[3] = 5; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "CheapWorkUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 20
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Add("CheapWorkUpgrade1");

        //Set name
        upgradeName = "CheapWorkUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 21
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 15; //Eau
        upgradeCost[2] = 15; //Resine
        upgradeCost[3] = 15; //Pollen

        //Set prereqs
        prereqs.Add("CheapWorkUpgrade2");

        //Set name
        upgradeName = "CheapWorkUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();


        //Upgrade 22
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "CheapScoutUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 23
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 20; //Nectar
        upgradeCost[1] = 20; //Eau
        upgradeCost[2] = 20; //Resine
        upgradeCost[3] = 20; //Pollen

        //Set prereqs
        prereqs.Add("CheapScoutUpgrade1");

        //Set name
        upgradeName = "CheapScoutUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 24
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 30; //Nectar
        upgradeCost[1] = 30; //Eau
        upgradeCost[2] = 30; //Resine
        upgradeCost[3] = 30; //Pollen

        //Set prereqs
        prereqs.Add("CheapScoutUpgrade2");

        //Set name
        upgradeName = "CheapScoutUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 25
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 15; //Nectar
        upgradeCost[1] = 15; //Eau
        upgradeCost[2] = 15; //Resine
        upgradeCost[3] = 15; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "ObjectivesUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 26
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 30; //Nectar
        upgradeCost[1] = 30; //Eau
        upgradeCost[2] = 30; //Resine
        upgradeCost[3] = 30; //Pollen

        //Set prereqs
        prereqs.Add("ObjectivesUpgrade1");

        //Set name
        upgradeName = "ObjectivesUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 27
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 45; //Nectar
        upgradeCost[1] = 45; //Eau
        upgradeCost[2] = 45; //Resine
        upgradeCost[3] = 45; //Pollen

        //Set prereqs
        prereqs.Add("ObjectivesUpgrade2");

        //Set name
        upgradeName = "ObjectivesUpgrade3";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 28
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 10; //Nectar
        upgradeCost[1] = 10; //Eau
        upgradeCost[2] = 10; //Resine
        upgradeCost[3] = 10; //Pollen

        //Set prereqs
        prereqs.Clear();

        //Set name
        upgradeName = "StartWorkUpgrade1";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 29
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 20; //Nectar
        upgradeCost[1] = 20; //Eau
        upgradeCost[2] = 20; //Resine
        upgradeCost[3] = 20; //Pollen

        //Set prereqs
        prereqs.Add("StartWorkUpgrade1");

        //Set name
        upgradeName = "StartWorkUpgrade2";

        //Don't touch this
        newUpgrade = new Upgrade(prereqs, upgradeCost);
        upgrades.Add(upgradeName, newUpgrade);
        prereqs.Clear();

        //Upgrade 30
        upgradeCost = new int[4];

        //Set costs
        upgradeCost[0] = 30; //Nectar
        upgradeCost[1] = 30; //Eau
        upgradeCost[2] = 30; //Resine
        upgradeCost[3] = 30; //Pollen

        //Set prereqs
        prereqs.Add("StartWorkUpgrade2");

        //Set name
        upgradeName = "StartWorkUpgrade3";

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

            if (!upgrades[name].upgradeUnlocked &&
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
                    //Done
                    case "ScoutVisionUpgrade1":
                        HexUnit.ScoutVisionUpgrade1 = true;
                        gameManager.grid.ResetVisibility();
                        break;
                    case "ScoutVisionUpgrade2":
                        HexUnit.ScoutVisionUpgrade2 = true;
                        gameManager.grid.ResetVisibility();
                        break;
                    case "ScoutVisionUpgrade3":
                        HexUnit.ScoutVisionUpgrade3 = true;
                        gameManager.grid.ResetVisibility();
                        break;

                    //Done
                    case "ScoutMoveUpgrade1":
                        HexUnit.ScoutMoveUpgrade1 = true;
                        break;
                    case "ScoutMoveUpgrade2":
                        HexUnit.ScoutMoveUpgrade2 = true;
                        break;
                    case "ScoutMoveUpgrade3":
                        HexUnit.ScoutMoveUpgrade3 = true;
                        break;
                    
                    //Done
                    case "ScoutFlyUpgrade1":
                        HexUnit.ScoutFlyUpgrade1 = true;
                        break;
                    case "ScoutFlyUpgrade2":
                        HexUnit.ScoutFlyUpgrade2 = true;
                        break;
                    case "ScoutFlyUpgrade3":
                        HexUnit.ScoutFlyUpgrade3 = true;
                        break;

                    case "ObjectivesUpgrade1":
                        GlobalObjectives.ObjectivesUpgrade1 = true;
                        gameManager.globalObjectives.UpdateObjectives();
                        break;
                    case "ObjectivesUpgrade2":
                        GlobalObjectives.ObjectivesUpgrade2 = true;
                        gameManager.globalObjectives.UpdateObjectives();
                        break;
                    case "ObjectivesUpgrade3":
                        GlobalObjectives.ObjectivesUpgrade3 = true;
                        gameManager.globalObjectives.UpdateObjectives();
                        break;

                    case "ProtectPesticideUpgrade1":
                        ResourcePoint.ProtectPesticideUpgrade1 = true;   
                        break;
                    case "ProtectPesticideUpgrade2":
                        ResourcePoint.ProtectPesticideUpgrade2 = true;
                        break;
                    case "ProtectPesticideUpgrade3":
                        ResourcePoint.ProtectPesticideUpgrade3 = true;
                        break;

                    case "ProtectWaspsUpgrade1":
                        ResourcePoint.ProtectWaspsUpgrade1 = true;
                        break;
                    case "ProtectWaspsUpgrade2":
                        ResourcePoint.ProtectWaspsUpgrade2 = true;
                        break;
                    case "ProtectWaspsUpgrade3":
                        ResourcePoint.ProtectWaspsUpgrade3 = true;
                        break;

                    case "GatherMoreUpgrade1":
                        ResourcePoint.GatherMoreUpgrade1 = true;
                        break;
                    case "GatherMoreUpgrade2":
                        ResourcePoint.GatherMoreUpgrade2 = true;
                        break;
                    case "GatherMoreUpgrade3":
                        ResourcePoint.GatherMoreUpgrade3 = true;
                        break;

                    //Done
                    case "StartWorkUpgrade1":
                        PlayerResources.StartWorkUpgrade1 = true;
                        break;
                    case "StartWorkUpgrade2":
                        PlayerResources.StartWorkUpgrade2 = true;
                        break;
                    case "StartWorkUpgrade3":
                        PlayerResources.StartWorkUpgrade3 = true;
                        break;

                    //Done
                    case "CheapWorkUpgrade1":
                        SpawnManager.CheapWorkUpgrade1 = true;
                        break;
                    case "CheapWorkUpgrade2":
                        SpawnManager.CheapWorkUpgrade2 = true;
                        break;
                    case "CheapWorkUpgrade3":
                        SpawnManager.CheapWorkUpgrade3 = true;
                        break;

                    //Done
                    case "CheapScoutUpgrade1":
                        SpawnManager.CheapScoutUpgrade1 = true;
                        break;
                    case "CheapScoutUpgrade2":
                        SpawnManager.CheapScoutUpgrade2 = true;
                        break;
                    case "CheapScoutUpgrade3":
                        SpawnManager.CheapScoutUpgrade3 = true;
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

    public void ResetAllUpgrades()
    {
        HexUnit.ScoutVisionUpgrade1 = false;
        HexUnit.ScoutVisionUpgrade2 = false;
        HexUnit.ScoutVisionUpgrade3 = false;

        HexUnit.ScoutMoveUpgrade1 = false;
        HexUnit.ScoutMoveUpgrade2 = false;
        HexUnit.ScoutMoveUpgrade3 = false;

        HexUnit.ScoutFlyUpgrade1 = false;
        HexUnit.ScoutFlyUpgrade2 = false;
        HexUnit.ScoutFlyUpgrade3 = false;

        GlobalObjectives.ObjectivesUpgrade1 = false;
        GlobalObjectives.ObjectivesUpgrade2 = false;
        GlobalObjectives.ObjectivesUpgrade3 = false;

        ResourcePoint.ProtectPesticideUpgrade1 = false;
        ResourcePoint.ProtectPesticideUpgrade2 = false;
        ResourcePoint.ProtectPesticideUpgrade3 = false;

        ResourcePoint.ProtectWaspsUpgrade1 = false;
        ResourcePoint.ProtectWaspsUpgrade2 = false;
        ResourcePoint.ProtectWaspsUpgrade3 = false;

        ResourcePoint.GatherMoreUpgrade1 = false;
        ResourcePoint.GatherMoreUpgrade2 = false;
        ResourcePoint.GatherMoreUpgrade3 = false;

        PlayerResources.StartWorkUpgrade1 = false;
        PlayerResources.StartWorkUpgrade2 = false;
        PlayerResources.StartWorkUpgrade3 = false;

        SpawnManager.CheapWorkUpgrade1 = false;
        SpawnManager.CheapWorkUpgrade2 = false;
        SpawnManager.CheapWorkUpgrade3 = false;

        SpawnManager.CheapScoutUpgrade1 = false;
        SpawnManager.CheapScoutUpgrade2 = false;
        SpawnManager.CheapScoutUpgrade3 = false;
    }



}
