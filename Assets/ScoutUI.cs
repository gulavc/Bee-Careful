using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutUI : MonoBehaviour {

    public ParticleSystem workersPrefab;
    [HideInInspector]
    public HexCell currentCell;

    public void SummonWorker()
    {
        ParticleSystem workers = Instantiate(workersPrefab);
        workers.transform.position = currentCell.transform.position;
        workers.Play();
        Destroy(workers, 5f);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
