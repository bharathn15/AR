using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFunctionality : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prefab;

    [Header("Default Material")]
    [SerializeField] private Material defaultMaterial;

    [Header("Transparent Material")]
    [SerializeField] private Material transParentMaterial;

    /// <summary>
    /// Set Material to its Default State..
    /// </summary>
    private bool isMaterialDefault;

    void Start()
    {
        isMaterialDefault = true; 
    }

    
    void Update()
    {
        //** Material Updation....
        MaterialUpdation();

    }

    void MaterialUpdation()
    {
        switch (isMaterialDefault)
        {
            case true:
                SetDefaultMaterial();
                break;
            case false:
                SetTransParentMaterial();
                break;
        }
    }

    /// <summary>
    /// Set Material to Transparent..
    /// </summary>
    void SetTransParentMaterial()
    {
        Material mat = prefab.GetComponent<MeshRenderer>().materials[0];
        if (mat != null)
        {
            mat.CopyPropertiesFromMaterial(transParentMaterial);
        }
        else
        {
            Debug.LogError("No material found.......");
        }
    } 

    /// <summary>
    /// Set Material to Default..
    /// </summary>
    void SetDefaultMaterial()
    {
        Material mat = prefab.GetComponent<MeshRenderer>().materials[0];
        if(mat != null)
        {
            mat.CopyPropertiesFromMaterial(defaultMaterial);
        }
        else
        {
            Debug.LogError("No material found.......");
        }
    }

    /// <summary>
    /// Change the Material Property on Click of the Button..
    /// </summary>
    public void ChangeMaterialProperties()
    {
        if (isMaterialDefault)
        {
            isMaterialDefault = false;
        }
        else
        {
            isMaterialDefault = true;
        }
        
    }
}
