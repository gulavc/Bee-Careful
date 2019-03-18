using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveClick : MonoBehaviour {

private bool isPressed;

	// Use this for initialization
	void Start () {
		
		isPressed = false;
	}
	
	// Update is called once per frame
	void BuyUpgrade () {
		
		isPressed = true;
		
	}
}
