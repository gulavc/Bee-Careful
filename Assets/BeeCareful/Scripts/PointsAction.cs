using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAction : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    
    public int pointsActionMax;
    private int pointsActionMaxBase;
    [Range(0, 1)]
    public float bonusEnergyPercentPerScout;

    private int currentPoints;
    public int Points { get; private set; }

	// Use this for initialization
	void Awake ()
    {
        pointsActionMaxBase = Points = pointsActionMax;
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void SetPointsAction(int pa, bool overrideLimit = false)
    {
        if((pa >= 0 && pa <= pointsActionMax) || overrideLimit)
        {
            Points = pa;
        }
        gameManager.UpdatePointsActionHUD();
    }

    public void RemovePointsAction(int change)
    {
        if(Points - change <= 0)
        {            
            Points = 0;
            gameManager.EndOfYear();
        }
        else if (Points - change > pointsActionMax && !Tutorial.earlyGameTutorial)
        {
            Points = pointsActionMax;
        }
        else
        {
            string currentSeason = GetCurrentSeason();
            Points -= change;
            string newSeason = GetCurrentSeason();
            if(newSeason != currentSeason)
            {
                gameManager.ChangeSeason(newSeason);
            }
        }
        gameManager.UpdatePointsActionHUD();
    }

    public void UpdateMaxPointsAction(int numScouts)
    {
        float current = Points / (float)pointsActionMax;
        pointsActionMax = (int)(pointsActionMaxBase * (1 + (numScouts-1) * bonusEnergyPercentPerScout));
        Points = (int)(current * pointsActionMax);
    }

    public string GetCurrentSeason()
    {
        if(Points > pointsActionMax * (2 / 3f))
        {
            return "Spring";
        }
        else if (Points > pointsActionMax * (1 / 3f))
        {
            return "Summer";
        }
        else
        {
            return "Autumn";
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if(bonusEnergyPercentPerScout > 1f)
        {
            bonusEnergyPercentPerScout = 1f;
        }
        else if(bonusEnergyPercentPerScout < 0f)
        {
            bonusEnergyPercentPerScout = 0f;
        }
    }
#endif
}
