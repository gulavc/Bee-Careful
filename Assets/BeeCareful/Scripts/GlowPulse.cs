using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowPulse : MonoBehaviour {

    public Color defaultColor;
    public float baseScale = 1f;
    public float period = 1f;
    public float amplitude = 0.3f;

    Image im;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        float scaleValue = baseScale + ((Mathf.PingPong(Time.time, period) / period) * amplitude);
        im.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
	}

    public void Show(Color color)
    {
        if (!im)
        {
            im = GetComponentInChildren<Image>();
        }
        im.color = color;
        this.gameObject.SetActive(true);
    }

    public void Show()
    {
        Show(defaultColor);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void MoveFocus(Vector3 position)
    {
        if (!im)
        {
            im = GetComponentInChildren<Image>();
        }
        im.transform.position = position;
    }
}
