using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAlert : MonoBehaviour {

    public UpgradeManager upgradeManager;


    private List<string> upgradeList;
    private List<string> upgradeNames;

    private Vector3 startPosition;

    public float showTime = 3f;
    
    // Use this for initialization
    void Start () {
        upgradeNames = new List<string>();
        upgradeList = new List<string>();
        startPosition = this.transform.position;

        //TEST
        Show();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool VerifyPopup()
    {
        if (upgradeNames.Count == 0)
        {
            upgradeNames = upgradeManager.GetUpgradeNames();
        }

        List<string> newUpgrades = new List<string>();

        foreach(string upgrade in upgradeNames)
        {
            if (VerifyUpgrade(upgrade))
            {
                newUpgrades.Add(upgrade);
            }
        }

        bool shouldPopup = false;

        foreach(string newUp in newUpgrades)
        {
            if (!upgradeList.Contains(newUp))
            {
                shouldPopup = true;
            }
        }

        upgradeList = newUpgrades;

        return shouldPopup;
    }


    private bool VerifyUpgrade(string s)
    {
        if (upgradeManager.IsUpgradeAcquired(s))
        {
            return false;
        }

        if(upgradeManager.IsUpgradeBuyable(s) && upgradeManager.IsUpgradeAvailable(s))
        {
            return true;
        }

        return false;
    }


    public void Show()
    {
        StartCoroutine(Translate(Vector2.right));
    }

    public void Hide()
    {

    }

    IEnumerator Translate(Vector2 direction)
    {
        while (true)
        {
            this.transform.Translate(Vector3.right);

            yield return null;
        }
    }

}
