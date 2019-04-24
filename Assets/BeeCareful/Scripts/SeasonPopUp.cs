using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonPopUp : MonoBehaviour {

    public Text seasonText, nectarText, waterText, resinText, pollenText;
    public float stayTime, moveSpeed;

    public Color springColor, summerColor, fallColor, pollenColor, nectarColor, waterColor, resinColor;

    private Vector3 velocity;
    private GameManager gameManager;
    private AudioClip seasonMusic;
    public AudioClip springMusic, summerMusic, fallMusic;

	// Use this for initialization
	void Start () {
        nectarText.color = nectarColor;
        waterText.color = waterColor;
        pollenText.color = pollenColor;
        resinText.color = resinColor;
        seasonMusic = springMusic;
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
                seasonMusic = springMusic;
                break;
            case "summer":
                seasonText.color = summerColor;
                seasonMusic = summerMusic;
                break;
            default:
                seasonText.color = fallColor;
                seasonMusic = fallMusic;
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
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        this.gameObject.SetActive(true);
        gameManager.PlaySFX(seasonMusic);

        yield return new WaitForSeconds(stayTime);

        this.gameObject.SetActive(false);
    }
}
