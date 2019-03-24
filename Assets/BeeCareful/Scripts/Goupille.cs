using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goupille : MonoBehaviour {

    HexMapCamera hexMapCamera;
    Transform attachedObject;
    SpriteRenderer spriteRenderer;
    public float upperLimit;
    public float lowerLimit;

    private float regression, intercept;

    // Use this for initialization
    void Start () {
        hexMapCamera = FindObjectOfType<HexMapCamera>();
        attachedObject = transform.parent;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        regression = 1f / (upperLimit - lowerLimit);
        intercept = 1 - regression * upperLimit;
    }
	
	// Update is called once per frame
	void Update () {
        //Rotation
        transform.localRotation = Quaternion.Euler(0f, -attachedObject.rotation.eulerAngles.y + hexMapCamera.Rotation, 0f);

        //Scale
        float newScale = Mathf.Lerp(1f, 0.2f, hexMapCamera.Zoom);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        //Fade        
        float lerp = (regression * hexMapCamera.Zoom) + intercept;

        Color c = spriteRenderer.color;
        c.a = Mathf.Lerp(0f, 1f, lerp);
        spriteRenderer.color = c;        

    }
}
