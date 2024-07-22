using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSystem: MonoBehaviour
{
    private static List<FurnitureSO> _allFurniture;
    public static List<FurnitureSO> AllFurniture => _allFurniture;

    private void Awake()
    {
        _allFurniture = new List<FurnitureSO>(Resources.LoadAll<FurnitureSO>("Prefabs/Furniture/SO"));
    }
}
