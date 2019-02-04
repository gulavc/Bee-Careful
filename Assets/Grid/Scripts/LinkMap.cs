using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LinkMap : HexInteractable {

    public string mapName;
    public string saveFolder;
    private HexGrid hexGrid;


public override void OnUnitEnterCell(HexCell cell)
    {
        string path = Path.Combine(Application.dataPath, saveFolder, mapName + ".map");
        Load(path);
    }
   

    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File does not exist " + path);
            return;
        }
        using (
            BinaryReader reader = new BinaryReader(File.OpenRead(path))
        )
        {
            int header = reader.ReadInt32();
            if (header <= 2)
            {
                hexGrid = GameObject.FindObjectOfType<HexGrid>();
                hexGrid.Load(reader, header);
                HexMapCamera.ValidatePosition();
            }
            else
            {
                Debug.LogWarning("Unknown map format " + header);
            }
        }
    }

    
}
