using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInScript : MonoBehaviour {

    SpriteRenderer rend;
    public GameObject button1;
    public GameObject nextButton;

    // Use this for initialization
    void Start() {
        rend = GetComponent<SpriteRenderer>();
        
    }

    IEnumerator FadeIn()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void StartFading()
    {
        button1.SetActive(false);
        if (nextButton)
        {
            nextButton.SetActive(true);
        }
            StartCoroutine("FadeIn");
    }
        
 }
