using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FurnitureTypesList;

//[CreateAssetMenu(fileName = "FurnitureScriptbleObject", menuName = "SO/Task")]
public class TaskSO
{
    private string _name;
    private RoomType _roomType;
    private Theme _theme;
    private FurnitureColor _color;

    public string Name => _name;
    public RoomType RoomType => _roomType;
    public Theme Theme => _theme;
    public FurnitureColor Colors => _color;

    public TaskSO (string name, RoomType roomType, Theme theme, FurnitureColor color)
    {
        _name = name;
        _roomType = roomType;
        _theme = theme;
        _color = color;
    }
}
