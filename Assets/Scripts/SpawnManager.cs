﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    //public Button SpawnScout;
    //public Button SpawnWorker;
    private HexGrid hexGrid;

    // Use this for initialization
    void Start()
    {

        //SpawnScout.onClick.AddListener(CreateScout);
        /*SpawnWorker.onClick.AddListener(CreateWorker);*/
        hexGrid = GameObject.FindObjectOfType<HexGrid>();


    }


    void Update()

    {
        if (Input.GetKeyDown(KeyCode.S))

        {

            CreateScout();

        }


        if (Input.GetKeyDown(KeyCode.W))

        {

            CreateWorker();

        }


    }



    public void CreateScout()
    {
        Debug.Log("Let's explore this hood!");
        HexCell cell = hexGrid.GetCell(new Vector3(1, -2, 1));
        if (cell && !cell.Unit)
        {
            hexGrid.AddUnit(
                Instantiate(HexUnit.unitPrefab), cell, Random.Range(0f, 360f)
            );

        }
    }


    public void CreateWorker()
    {

        Debug.Log("Let's gather some shit!");

    }
}   


