using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAction : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    
    public int pointsActionMax;
    [Range(0, 1)]
    public float bonusEnergyPercentPerScout;

    public int Points { get; private set; }

	// Use this for initialization
	void Awake ()
    {
        Points = pointsActionMax;

    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void SetPointsAction(int pa)
    {
        if(pa >= 0 && pa <= pointsActionMax)
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
        else if (Points - change > pointsActionMax)
        {
            Points = pointsActionMax;
        }
        else
        {
            Points -= change;
        }
        gameManager.UpdatePointsActionHUD();
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
