using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goupille : MonoBehaviour {

    HexMapCamera hexMapCamera;
    Transform attachedObject;
    SpriteRenderer spriteRenderer;
    HexGrid grid;
    bool hide = false;

    public float upperLimit;
    public float lowerLimit;
    public GoupilleType type;

    private float regression, intercept;

    // Use this for initialization
    void Start () {
        hexMapCamera = FindObjectOfType<HexMapCamera>();
        attachedObject = transform.parent;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        grid = FindObjectOfType<HexGrid>();

        regression = 1f / (upperLimit - lowerLimit);
        intercept = 1 - regression * upperLimit;    

    }
	
	// Update is called once per frame
	void Update () {
        if (!hide)
        {
            bool show = false;
            switch (type)
            {
                case GoupilleType.BEE:
                    if (Options.ShowBeePins)
                    {
                        show = true;
                    }
                    break;
                case GoupilleType.DANGER:
                    if (Options.ShowDangerPins)
                    {
                        show = true;
                    }
                    break;
                case GoupilleType.HIVE:
                    if (Options.ShowHivePins)
                    {
                        show = true;
                    }
                    break;
                case GoupilleType.MOUNTAIN:
                    if (Options.ShowMountainPins)
                    {
                        show = true;
                    }
                    break;
                case GoupilleType.RESOURCE:
                    if (Options.ShowResourcePins)
                    {
                        show = true;
                    }
                    break;
                default:
                    break;
            }

            if (show)
            {
                UpdateGoupille();
            }
            else
            {
                HideGoupille();
            }
        }
        else
        {
            HideGoupille();
        }
            

    }

    void UpdateGoupille()
    {        
        //Rotation
        transform.localRotation = Quaternion.Euler(0f, -attachedObject.rotation.eulerAngles.y + hexMapCamera.Rotation, 0f);

        //Scale
        float newScale = Mathf.Lerp(1f, 0.2f, hexMapCamera.Zoom);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        //Fade        
        float lerp = (regression * hexMapCamera.Zoom) + intercept;

        Color c = spriteRenderer.color;

        if (grid.GetCell(attachedObject.position).IsExplored)
        {
            c.a = Mathf.Lerp(0f, 1f, lerp);
        }
        else
        {
            c.a = 0f;
        }

        spriteRenderer.color = c;
    }

    void HideGoupille()
    {
        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;
    }

    public void Hide()
    {
        hide = true;
    }

    public void Show()
    {
        hide = false;
    }


    public enum GoupilleType
    {
        BEE,
        HIVE,
        RESOURCE,
        MOUNTAIN,
        DANGER
    }
}
