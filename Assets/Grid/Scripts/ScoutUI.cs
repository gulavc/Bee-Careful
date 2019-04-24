using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutUI : MonoBehaviour
{

    public ParticleSystem workersPrefab;
    public Button gatherButton;
    public AudioClip notEnoughWorkersSFX;
    [HideInInspector]
    public HexCell currentCell;
    [HideInInspector]
    public ResourcePoint resourcePoint;
    [HideInInspector]
    public GameManager gameManager;

    private HexCell hiveCell;
    private GameObject workersHolder;

    public void SummonWorker()
    {

        resourcePoint = GetResourcePoint();

        if (resourcePoint)
        {
            if (resourcePoint.GatherResources())
            {
                StartCoroutine(WorkersTravel());
            }
            else
            {
                gameManager.PlaySFX(notEnoughWorkersSFX);
            }
        }
        else
        {
            Debug.Log("WTF");
        }

    }

    IEnumerator WorkersTravel()
    {

        if (!hiveCell)
        {
            hiveCell = gameManager.HiveCell;
        }

        Vector3 target = resourcePoint.transform.position;

        ParticleSystem workers = GetWorkerFromPool();
        workers.transform.position = hiveCell.transform.position;
        workers.Play();

        
        Vector3 velocity = Vector3.zero;
        float smoothTime = 2f;

        //Go to resource point
        while (Vector3.Distance(workers.transform.position, target) > 0.1)
        {
            workers.transform.position = Vector3.SmoothDamp(workers.transform.position, target, ref velocity, smoothTime);
            yield return null;
        }

        target = hiveCell.transform.position;

        //Gather for a couple seconds
        yield return new WaitForSeconds(4f);

        //Go back to hive
        while (Vector3.Distance(workers.transform.position, target) > 0.1)
        {
            workers.transform.position = Vector3.SmoothDamp(workers.transform.position, target, ref velocity, smoothTime);
            yield return null;
        }

        //Reclaim
        ReclaimWorker(workers);

    }

    // Use this for initialization
    void Start()
    {
        workersPool = new Queue<ParticleSystem>();
        workersHolder = new GameObject();
        workersHolder.name = "WorkersHolder";
        workersHolder.transform.parent = this.transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideButton()
    {
        gatherButton.gameObject.SetActive(false);
    }

    public void ShowButton()
    {
        gatherButton.gameObject.SetActive(true);
    }

    ResourcePoint GetResourcePoint()
    {
        foreach (ResourcePoint rp in GameObject.FindObjectsOfType<ResourcePoint>())
        {
            if (rp.Cell == currentCell)
            {
                return rp;
            }
        }

        return null;
    }


    //WorkersPool
    Queue<ParticleSystem> workersPool;

    public ParticleSystem GetWorkerFromPool()
    {
        if (workersPool.Count > 0)
        {
            ParticleSystem p = workersPool.Dequeue();
            p.gameObject.SetActive(true);
            return p;
        }

        ParticleSystem ps = Instantiate(workersPrefab);
        ps.transform.SetParent(workersHolder.transform);
        return ps;
    }

    public void ReclaimWorker(ParticleSystem ps)
    {
        ps.gameObject.SetActive(false);
        workersPool.Enqueue(ps);
    }



}
