using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : HexInteractable {

    private GameManager gameManager;
    private ResourcePointManager rpm;
    private AudioSource sound;
    public ResourceType type;
    //public int resourceValue;
    public int resourceMax;
    public int workforceCost;

    public bool hasWasp = false;
    public bool hasPesticide = false;

    public ParticleSystem gatherParticles;
    public AudioClip soundToPlay;
    private GameObject UITarget;
    

    public static bool ProtectPesticideUpgrade1 = false;
    public static bool ProtectPesticideUpgrade2 = false;
    public static bool ProtectPesticideUpgrade3 = false;

    public static bool ProtectWaspsUpgrade1 = false;
    public static bool ProtectWaspsUpgrade2 = false;
    public static bool ProtectWaspsUpgrade3 = false;

    public static bool GatherMoreUpgrade1 = false;
    public static bool GatherMoreUpgrade2 = false;
    public static bool GatherMoreUpgrade3 = false;
    
    public float GatherPercent {
        get {
            if (GatherMoreUpgrade3)
            {
                return 1.3f;
            }
            else if (GatherMoreUpgrade2)
            {
                return 1.2f;
            }
            else if (GatherMoreUpgrade1)
            {
                return 1.1f;
            }
            else
            {
                return 1.0f;
            }            
        }
    }

    public float WaspProtection {
        get {
            if (ProtectWaspsUpgrade3)
            {
                return 1f;
            }
            else if (ProtectWaspsUpgrade2)
            {
                return 2 / 3f;
            }
            else if (ProtectWaspsUpgrade1)
            {
                return 1 / 3f;
            }
            else
            {
                return 0f;
            }
        }
    }

    public float PesticideProtection {
        get {
            if (ProtectPesticideUpgrade3)
            {
                return 1f;
            }
            else if (ProtectPesticideUpgrade2)
            {
                return 2 / 3f;
            }
            else if (ProtectPesticideUpgrade1)
            {
                return 1 / 3f;
            }
            else
            {
                return 0f;
            }
        }
    }

    [HideInInspector]
    public GameObject dangerPrefab;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rpm = GameObject.FindObjectOfType<ResourcePointManager>();
        sound = gameObject.AddComponent<AudioSource>();
        
        rpm.AddResourcePoint(this);
        RemainingResources = resourceMax;

        Cell = gameManager.grid.GetCell(HexCoordinates.FromPosition(transform.position));

    }

    protected override void Update()
    {
        base.Update();

        if (dangerPrefab)
        {
            if (Cell.IsVisible)
            {
                dangerPrefab.SetActive(true);
                if (Options.ShowDangerPins)
                {
                    HideGoupille();
                }
                else
                {
                    ShowGoupille();
                }
                
            }
            else
            {
                dangerPrefab.SetActive(false);
                ShowGoupille();
            }
        }
    }


    public bool GatherResources() {

        int actualWorkforceCost = workforceCost;
        if (hasPesticide)            
        {
            float pesticidePenalty = 1 - rpm.pesticidePenalty;
            pesticidePenalty *= (1 - PesticideProtection);
            pesticidePenalty += 1;

            actualWorkforceCost = (int)(actualWorkforceCost * pesticidePenalty);
        }

        if (gameManager.GetRessourceCount(ResourceType.Workers) >= actualWorkforceCost)
        {
            int resourceGet = Mathf.CeilToInt(RemainingResources * GatherPercent);
            RemainingResources -= resourceGet;
            //si c'est 0 ou plus bas, tu peux rien ramasser.            
            if (RemainingResources <= 0)
            {
                Cell.SpecialIndex = 0;
                gameManager.HideScoutUI();
            }

            if (hasWasp)
            {
                float waspPenalty = rpm.waspPenalty;
                waspPenalty *= (1 - WaspProtection);
                resourceGet = (int)(resourceGet * (1 - waspPenalty)); 
            }

            //Register Bonus According to Resource Type
            rpm.TapResourcePoint(Cell, type);

            gameManager.RemovePlayerRessources(ResourceType.Workers, actualWorkforceCost);
            gameManager.AddPlayerResources(type, resourceGet);
            sound.PlayOneShot(soundToPlay);

            //Test
            ParticleSystem anim = Instantiate(gatherParticles);
            Destroy(anim.gameObject, 5f);
            //anim.emissionRate *= resourceGet / (resourceMax * GatherPercent);
            anim.transform.position = this.transform.position;
            anim.Play();

            float scale = RemainingResources / (float)resourceMax;
            this.transform.localScale = new Vector3(scale, scale, scale);

            return true;
            
        }
        return false;
    }

    public override void OnUnitEnterCell(HexCell cell)
    {
        if (hasWasp)
        {
            gameManager.ShowWaspsTutorial();
        }
        if (hasPesticide)
        {
            gameManager.ShowPesticideTutorial();
        }
    }

    public void HideGoupille()
    {
        GetComponentInChildren<Goupille>().Hide();
    }

    public void ShowGoupille()
    {
        GetComponentInChildren<Goupille>().Show();
    }

    public int RemainingResources { get; private set; }

}
