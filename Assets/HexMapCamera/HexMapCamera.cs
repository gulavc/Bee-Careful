using System;
using System.Collections;
using UnityEngine;

public class HexMapCamera : MonoBehaviour
{

    Transform swivel, stick;

    float zoom = 1f;
    float rotationAngle;

    public float Rotation {
        get {
            return rotationAngle;
        }
    }

    public float Zoom {
        get {
            return zoom;
        }
    }

    public float stickMinZoom, stickMaxZoom;
    public float swivelMinZoom, swivelMaxZoom;

    public float moveSpeedMinZoom, moveSpeedMaxZoom;

    public float rotationSpeed;

    public HexGrid grid;

    static HexMapCamera instance;

    private Vector3 velocity = Vector3.zero;

    [Header("Time for the camera to move to target on autonomous move")]
    public float smoothTime = 0.3F;
    const float minDistance = 0.1f;

    void Awake()
    {
        instance = this;
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
    }

    void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        float xDelta, zDelta;
        if (zoomDelta != 0f)
        {
            if (zoomDelta > 0f && zoom < 1f)
            {
                xDelta = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 10);
                zDelta = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 10);
                AdjustPosition(xDelta, zDelta);
            }
            AdjustZoom(zoomDelta);
        }
        
        float rotationDelta = Input.GetAxis("Rotation");
        if (rotationDelta != 0f)
        {            
            AdjustRotation(rotationDelta);                       
        }

        xDelta = Input.GetAxis("Horizontal");
        zDelta = Input.GetAxis("Vertical");
        if (xDelta != 0f || zDelta != 0f)
        {
            AdjustPosition(xDelta, zDelta);
        }
    }

    void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);

        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0f, 0f, distance);

        float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
        swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 direction =
            transform.localRotation * 
            new Vector3(xDelta, 0f, zDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        float distance = 
            Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * 
            damping * Time.deltaTime;

        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = ClampPosition(position);
    }

    Vector3 ClampPosition(Vector3 position)
    {
        float xMax =
            (grid.cellCountX - 0.5f) *
            (2f * HexMetrics.innerRadius);
        position.x = Mathf.Clamp(position.x, 0f, xMax);

        float zMax =
            (grid.cellCountZ - 1) *
            (1.5f * HexMetrics.outerRadius);
        position.z = Mathf.Clamp(position.z, 0f, zMax);

        return position;
    }

    void AdjustRotation(float delta)
    {
        rotationAngle += delta * rotationSpeed * Time.deltaTime;
        if (rotationAngle < 0f)
        {
            rotationAngle += 360f;
        }
        else if (rotationAngle >= 360f)
        {
            rotationAngle -= 360f;
        }
        transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }

    public static bool Locked
    {
        set
        {
            instance.enabled = !value;
        }
    }

    public static void ValidatePosition()
    {
        instance.AdjustPosition(0f, 0f);
    }

    public static void MoveTo(HexCell cell)
    {
        instance.MoveCamera(cell);
        
        
    }

    void MoveCamera(HexCell cell)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothMove(cell));
    }

    IEnumerator SmoothMove(HexCell cell)
    {
        Vector3 targetPosition = cell.Position;
        float distance = Vector3.Distance(targetPosition, instance.transform.position);
       
        while (distance > minDistance)
        {
            instance.transform.localPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            distance = Vector3.Distance(targetPosition, instance.transform.position);
            yield return null;
        }
        yield break;
    }
}