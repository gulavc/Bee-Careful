using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HexUnit : MonoBehaviour, IUpgrade {

    public static bool ScoutVisionUpgrade1 = false;
    public static bool ScoutVisionUpgrade2 = false;
    public static bool ScoutVisionUpgrade3 = false;

    public static bool ScoutMoveUpgrade1 = false;
    public static bool ScoutMoveUpgrade2 = false;
    public static bool ScoutMoveUpgrade3 = false;

    public static bool ScoutFlyUpgrade1 = false;
    public static bool ScoutFlyUpgrade2 = false;
    public static bool ScoutFlyUpgrade3 = false;



    public Text countText;
    HexCell location, currentTravelLocation;
    float orientation;
    int speed = 200000;
    
    public int VisionRange {
        get {
            if (ScoutVisionUpgrade3)
            {
                return 6;
            }
            else if (ScoutVisionUpgrade2)
            {
                return 5;
            }
            else if (ScoutVisionUpgrade1)
            {
                return 4;
            }
            return 3;
        }
    }

    public static int MoveCost {
        get {
            if (ScoutMoveUpgrade3)
            {
                return 1;
            }
            else if (ScoutMoveUpgrade2)
            {
                return 2;
            }
            else if (ScoutMoveUpgrade1)
            {
                return 3;
            }
            return 4;
        }
    }


    List<HexCell> pathToTravel;
    const float travelSpeed = 4f; //cells per second
    const float rotationSpeed = 180f; //degrees per second

    private bool canFly = false;

    public static HexUnit unitPrefab;

    public HexGrid Grid { get; set; }

    public bool IsControllable {
        get {
            return (Grid.GetCell(transform.position) == location);
        }
    }

    void OnEnable() {
        if (location) {
            transform.localPosition = location.Position;
            if (currentTravelLocation)
            {
                Grid.IncreaseVisibility(location, VisionRange);
                Grid.DecreaseVisibility(currentTravelLocation, VisionRange);
                currentTravelLocation = null;
            }
        }
    }

    public HexCell Location {
        get {
            return location;
        }
        set {
            if (location) {
                Grid.DecreaseVisibility(location, VisionRange);
                location.Unit = null;
            }
            location = value;
            value.Unit = this;
            Grid.IncreaseVisibility(value, VisionRange);
            transform.localPosition = value.Position;
        }
    }

    public float Orientation {
        get {
            return orientation;
        }
        set {
            orientation = value;
            transform.localRotation = Quaternion.Euler(0f, value, 0f);
        }
    }    

    public int Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    public void ValidateLocation() {
        transform.localPosition = location.Position;
    }

    public void Die() {
        if (location)
        {
            Grid.DecreaseVisibility(location, VisionRange);
        }
        location.Unit = null;
        Destroy(gameObject);
    }

    public void Save(BinaryWriter writer) {
        location.coordinates.Save(writer);
        writer.Write(orientation);
    }

    public static void Load(BinaryReader reader, HexGrid grid) {
        HexCoordinates coordinates = HexCoordinates.Load(reader);
        float orientation = reader.ReadSingle();
        grid.AddUnit(
            Instantiate(unitPrefab), grid.GetCell(coordinates), orientation
        );
    }

    public bool IsValidDestination(HexCell cell) {
        return cell.IsExplored && !cell.Unit && (!cell.IsUnderwater || ScoutFlyUpgrade2);
    }

    public void UseMovement(int move) {
        speed -= move;
        if (speed < 0)
            speed = 0;
    }

    public void ResetMovement() {
        speed = 14;
    }

    IEnumerator LookAt(Vector3 point) {
        point.y = transform.localPosition.y;

        Quaternion fromRotation = transform.localRotation;
        Quaternion toRotation =
            Quaternion.LookRotation(point - transform.localPosition);
        float angle = Quaternion.Angle(fromRotation, toRotation);

        if (angle > 0f) {
            float speed = rotationSpeed / angle;

            for (
                float t = Time.deltaTime * speed;
                t < 1f;
                t += Time.deltaTime * speed
            ) {
                transform.localRotation =
                    Quaternion.Slerp(fromRotation, toRotation, t);
                yield return null;
            }
        }
        transform.LookAt(point);
        orientation = transform.localRotation.eulerAngles.y;
    }

    public void Travel(List<HexCell> path) {
        location.Unit = null;
        location = path[path.Count - 1];
        location.Unit = this;
        pathToTravel = path;
        StopAllCoroutines();
        StartCoroutine(TravelPath());
    }

    //Uncomment to see paths travelled by units drawn with gizmos
    /*void OnDrawGizmos() {
        if (pathToTravel == null || pathToTravel.Count == 0) {
            return;
        }

        Vector3 a, b, c = pathToTravel[0].Position;

        for (int i = 1; i < pathToTravel.Count; i++) {
            a = c;
            b = pathToTravel[i - 1].Position;
            c = (b + pathToTravel[i].Position) * 0.5f;
            for (float t = 0f; t < 1f; t += Time.deltaTime * travelSpeed) {
                Gizmos.DrawSphere(Bezier.GetPoint(a, b, c, t), 2f);
            }
        }

        a = c;
        b = pathToTravel[pathToTravel.Count - 1].Position;
        c = b;
        for (float t = 0f; t < 1f; t += 0.1f) {
            Gizmos.DrawSphere(Bezier.GetPoint(a, b, c, t), 2f);
        }
    }*/

    IEnumerator TravelPath() {
        Vector3 a, b, c = pathToTravel[0].Position;
        transform.localPosition = c;
        yield return LookAt(pathToTravel[1].Position);
        Grid.DecreaseVisibility(
            currentTravelLocation ? currentTravelLocation : pathToTravel[0],
            VisionRange
        );

        float t = Time.deltaTime * travelSpeed;
        for (int i = 1; i < pathToTravel.Count; i++) {
            currentTravelLocation = pathToTravel[i];
            a = c;
            b = pathToTravel[i - 1].Position;
            c = (b + currentTravelLocation.Position) * 0.5f;
            Grid.IncreaseVisibility(pathToTravel[i], VisionRange);
            for (; t < 1f; t += Time.deltaTime * travelSpeed) {
                transform.localPosition = Bezier.GetPoint(a, b, c, t);
                Vector3 d = Bezier.GetDerivative(a, b, c, t);
                d.y = 0f;
                transform.localRotation = Quaternion.LookRotation(d);
                yield return null;
            }
            Grid.DecreaseVisibility(pathToTravel[i], VisionRange);
            t -= 1f;
        }

        currentTravelLocation = null;

        a = c;
        b = location.Position;
        c = b;
        Grid.IncreaseVisibility(location, VisionRange);
        for (; t < 1f; t += Time.deltaTime * travelSpeed) {
            transform.localPosition = Bezier.GetPoint(a, b, c, t);
            Vector3 d = Bezier.GetDerivative(a, b, c, t);
            d.y = 0f;
            transform.localRotation = Quaternion.LookRotation(d);
            yield return null;
        }
        transform.localPosition = location.Position;
        orientation = transform.localRotation.eulerAngles.y;

        ListPool<HexCell>.Add(pathToTravel);
        pathToTravel = null;
    }

    public int GetMoveCost(HexCell fromCell, HexCell toCell, HexDirection direction)
    {
        int moveCost;

        HexEdgeType edgeType = fromCell.GetEdgeType(toCell);
        if (edgeType == HexEdgeType.Cliff && !ScoutFlyUpgrade1)
        {
            return -1;
        }
                
        /*if (fromCell.HasRoadThroughEdge(direction))
        {
            moveCost = 1;
        }
        else */
        if (fromCell.Walled != toCell.Walled && !ScoutFlyUpgrade1)
        {
            return -1;
        }
        if(toCell.UrbanLevel > 0 && !ScoutFlyUpgrade3)
        {
            return -1;
        }
        else
        {
            moveCost = MoveCost;
            /*moveCost = edgeType == HexEdgeType.Flat ? 3 : 5;
            moveCost += (toCell.UrbanLevel + toCell.FarmLevel + toCell.PlantLevel) / 2;*/
        }
        return moveCost;
    }


    public void ApplyUpgrade(string name)
    {
        switch (name)
        {
            case "ScoutVisionUpgrade1":
                break;
            case "ScoutFlyUpgrade":
                Debug.Log("UpgradeScoutFly");
                canFly = true;
                break;
            default:
                Debug.LogError("No such upgrade");
                break;
        }
    }

    

}
