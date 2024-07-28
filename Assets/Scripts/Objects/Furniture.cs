using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture: MonoBehaviour
{
    [SerializeField] private FurnitureSO _furnitureSO;

    public FurnitureSO FurnitureSO => _furnitureSO;
}
