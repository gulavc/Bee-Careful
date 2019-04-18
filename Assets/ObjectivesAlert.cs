using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesAlert : MonoBehaviour {

    public GameManager gameManager;

    public Text objectivesText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NectarObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Nectar) >= gameManager.globalObjectives.GetObjective(ResourceType.Nectar)))

        {

            objectivesText.text = "YEARLY OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText.text = "YEARLY OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Nectar) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Nectar));

        }


    }

    public void WaterObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Water) >= gameManager.globalObjectives.GetObjective(ResourceType.Water)))

        {

            objectivesText.text = "YEARLY OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText.text = "YEARLY OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Water) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Water));

        }


    }

    public void ResinObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Resin) >= gameManager.globalObjectives.GetObjective(ResourceType.Resin)))

        {

            objectivesText.text = "YEARLY OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText.text = "YEARLY OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Resin) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Resin));

        }


    }

    public void PollenObjective()

    {

        if ((gameManager.GetRessourceCount(ResourceType.Pollen) >= gameManager.globalObjectives.GetObjective(ResourceType.Pollen)))

        {

            objectivesText.text = "YEARLY OBJECTIVE: COMPLETE!";

        }

        else

        {

            objectivesText.text = "YEARLY OBJECTIVE: " + (gameManager.GetRessourceCount(ResourceType.Pollen) + " / " + gameManager.globalObjectives.GetObjective(ResourceType.Pollen));

        }


    }

}
