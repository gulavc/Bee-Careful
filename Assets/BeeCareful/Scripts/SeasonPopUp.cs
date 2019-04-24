﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonPopUp : MonoBehaviour {

    public Text seasonText, nectarText, waterText, resinText, pollenText;
    public float stayTime, moveSpeed;

    public Color springColor, summerColor, fallColor, pollenColor, nectarColor, waterColor, resinColor;

    private Vector3 velocity;



	// Use this for initialization
	void Start () {
        nectarText.color = nectarColor;
        waterText.color = waterColor;
        pollenText.color = pollenColor;
        resinText.color = resinColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetResources(int[] bonuses)
    {
        nectarText.text = "+" + bonuses[0];
        waterText.text = "+" + bonuses[1];
        resinText.text = "+" + bonuses[2];
        pollenText.text = "+" + bonuses[3];
    }

    public void SetSeasonName(string seasonName)
    {
        seasonText.text = seasonName;
        switch (seasonName.ToLower())
        {
            case "spring":
                seasonText.color = springColor;
                break;
            case "summer":
                seasonText.color = summerColor;
                break;
            default:
                seasonText.color = fallColor;
                break;
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ShowHide());
    }

    IEnumerator ShowHide()
    {
        this.gameObject.SetActive(true);

        yield return new WaitForSeconds(stayTime);

        this.gameObject.SetActive(false);
    }
}