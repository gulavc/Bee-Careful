﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject playButton, editButton, loadingText;
    public float rotationSpeed, scaleSpeed, amplitude, scaleAmp;

    [Header("Juice Stuff")]
    public GameObject logo;
    public AudioSource music;
    public Texture2D cursor;

    [Header("Credits")]
    public GameObject[] credits;
    public GameObject[] notCredits;

    public void PlayGame(int loadMode)
    {
        GameLoader.LoadMode = (LoadMode)loadMode; 
        SceneManager.LoadSceneAsync("TestWorld");

        playButton.SetActive(false);
        editButton.SetActive(false);

        loadingText.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    IEnumerator AnimLogo()
    {
        float deg = 0;
        for(; ; )
        {
            deg += Time.deltaTime * rotationSpeed;
            if(deg > 360f)
            {
                deg -= 360f;
            }
            logo.transform.Translate(Vector3.up * Mathf.Sin(deg) * amplitude);
            yield return new WaitForEndOfFrame();
        }
        
    }

    IEnumerator AnimButton()
    {
        float deg = 0;
        for (; ; )
        {
            deg += Time.deltaTime * scaleSpeed;
            if (deg > 360f)
            {
                deg -= 360f;
            }

            float newScale = 1 + (Mathf.Sin(deg) * scaleAmp);
            playButton.transform.localScale = new Vector3(newScale, newScale, newScale);

            yield return new WaitForEndOfFrame();
        }
        
    }

    public void ShowCredits()
    {
        foreach(GameObject g in notCredits)
        {
            g.SetActive(false);
        }

        foreach(GameObject g in credits)
        {
            g.SetActive(true);
        }
    }

    public void HideCredits()
    {
        foreach (GameObject g in notCredits)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in credits)
        {
            g.SetActive(false);
        }
    }

    private void Update()
    {
        if (!music.isPlaying)
        {
            music.time = 55f;
            music.Play();
        }

    }

    private void Start()
    {
        StartCoroutine(AnimLogo());
        StartCoroutine(AnimButton());
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}
