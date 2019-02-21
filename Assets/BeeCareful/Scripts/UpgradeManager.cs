using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public Dictionary<string, bool> upgrades;

    void Start()
    {
        upgrades = new Dictionary<string, bool>();

        upgrades.Add("ScoutVisionUpgrade", false);
    }


}
