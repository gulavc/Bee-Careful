using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexGridExplorationHolder
{

    private static List<bool> explorationData = new List<bool>();

    public static void SaveExplorationData(HexCell[] cells)
    {
        explorationData.Clear();
        foreach (HexCell c in cells)
        {
            explorationData.Add(c.IsExplored);
        }
    }

    public static List<bool> LoadExplorationData()
    {
        return explorationData;
    }


}
