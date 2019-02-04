using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAction : MonoBehaviour {


    
    public int pointsAction;
    public Text countText;

	// Use this for initialization
	void Start ()
    {
        pointsAction = 100;
        SetCountText();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetCountText ()
    {
        countText.text = "Points d'Action :" + countText.ToString();
    }
}
