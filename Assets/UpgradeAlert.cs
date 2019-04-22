using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAlert : MonoBehaviour {

    public UpgradeManager upgradeManager;


    private List<string> upgradeList;
    private List<string> upgradeNames;

    public float showTime = 5f;

    private float width;
    
    // Use this for initialization
    void Start () {

        upgradeNames = new List<string>();
        upgradeList = new List<string>();

        width = 0;
        RectTransform[] temp = GetComponentsInChildren<RectTransform>();
        foreach(RectTransform r in temp)
        {
            if(r.sizeDelta.x > width)
            {
                width = r.sizeDelta.x;
            }
        }        
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
        this.gameObject.SetActive(true);
        StartCoroutine(Translate(Vector2.right));
    }

    public void Hide()
    {
        StartCoroutine(WaitForHide());        
    }

    IEnumerator Translate(Vector2 direction, bool autoHide = true)
    {
        float translate = 0;
        while (translate <= width)
        {
            translate += width * Time.deltaTime;
            this.transform.Translate(direction * width * Time.deltaTime);

            yield return null;
        }

        if (autoHide)
        {
            yield return new WaitForSeconds(showTime);

            Hide();
        }
        
    }

    IEnumerator WaitForHide()
    {
        yield return StartCoroutine(Translate(Vector2.left, false));
        this.gameObject.SetActive(false);
    }

}
