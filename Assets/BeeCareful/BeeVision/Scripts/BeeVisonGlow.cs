using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeVisonGlow : MonoBehaviour
{

    public Material glowMaterial;

    private Material[][] currentMaterials;


    // Use this for initialization
    void Start()
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();

        currentMaterials = new Material[children.Length][];


        for (int i = 0; i < children.Length; i++)
        {
            Renderer rend = children[i];
            currentMaterials[i] = new Material[rend.materials.Length];
            for (int j = 0; j < rend.materials.Length; j++)
            {
                currentMaterials[i][j] = rend.materials[j];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            ChangeMaterial(glowMaterial);

        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            ChangeMaterial(currentMaterials);
        }
    }

    void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            Material[] mats = new Material[rend.materials.Length];
            for (int j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }

    void ChangeMaterial(Material[][] newMats)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < children.Length; i++) 
        {
            Renderer rend = children[i];
            Material[] mats = new Material[rend.materials.Length];
            for (int j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMats[i][j];
            }
            rend.materials = mats;
        }
    }


}
