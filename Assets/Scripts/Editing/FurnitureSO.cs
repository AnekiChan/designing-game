using UnityEngine;
using System;
using FurnitureTypesList;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FurnitureScriptbleObject", menuName = "SO/Furniture")]
public class FurnitureSO : ScriptableObject, IFurniture
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private FurnitureType _furnitureType;
    [SerializeField] private List<RoomType> _roomTypes = new List<RoomType>();
    [SerializeField] private List<Theme> _themes = new List<Theme>();
    [SerializeField] private List<MethodToOntaining> _methodsToObtaining = new List<MethodToOntaining>();
    [SerializeField] private List<FurnitureColor> _furnitureColors = new List<FurnitureColor>();

    public string Name => _name;
    public Sprite UIIcon => _uiIcon;
    public FurnitureType FurnitureType => _furnitureType;
    public List<RoomType> RoomTypes => _roomTypes;
    public List<Theme> Theme => _themes;
    public List<MethodToOntaining> MethodsToObtaining => _methodsToObtaining;
    public List<FurnitureColor> FurnitureColors => _furnitureColors;
}