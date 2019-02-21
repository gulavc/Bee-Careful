using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade {

    public bool upgradeUnlocked;
    public List<string> upgradePrerequisites;
    public int[] upgradeCost;

    public Upgrade(List<string> prereqs, int[] costNERP)
    {
        upgradeUnlocked = false;
        upgradePrerequisites = new List<string>(prereqs);
        upgradeCost = costNERP;
    }

    public void Unlock()
    {
        upgradeUnlocked = true;
    }

}
