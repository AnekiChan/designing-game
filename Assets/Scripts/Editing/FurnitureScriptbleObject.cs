using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FurnitureScriptbleObject", menuName = "ScriptbleObjects/furniture")]
public class FurnitureScriptbleObject : ScriptableObject, IFurniture
{
    public string Name => _name;
    public Sprite UIIcon => _uiIcon;
    public GameObject Pref => prefab;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;

    public GameObject prefab;

    public int rank;
    public int cost;
    public bool isSitting;
    public bool iSOccupied;

    [Header("Item type matching (from 1 to 5)")]
    public int cute;
    public int modern;
    public int classic;
}
