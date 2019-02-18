using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfYearUI : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

}
