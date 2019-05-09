using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesAlert : MonoBehaviour {

    public GameManager gameManager;

    public Text objectivesText1, objectivesText2, objectivesText3, objectivesText4;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        NectarObjective();
        PollenObjective();
        WaterObjective();
        ResinObjective();
	}


    public void NectarObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= gameManager.globalObjectives.GetObjective(ResourceType.Nectar)))

        {

            objectivesText1.text = "NECTAR OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText1.text = "NECTAR OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar));

        }


    }

    public void WaterObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Water) >= gameManager.globalObjectives.GetObjective(ResourceType.Water)))

        {

            objectivesText3.text = "WATER OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText3.text = "WATER OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water));

        }


    }

    public void ResinObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Resin) >= gameManager.globalObjectives.GetObjective(ResourceType.Resin)))

        {

            objectivesText4.text = "RESIN OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText4.text = "RESIN OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin));

        }


    }

    public void PollenObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Pollen) >= gameManager.globalObjectives.GetObjective(ResourceType.Pollen)))

        {

            objectivesText2.text = "POLLEN OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText2.text = "POLLEN OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen));

        }


    }

}
