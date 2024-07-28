using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomData
{
    public string Id;
    public Vector2 RoomPos;
    public string WallId;
    public string FloorId;
    public Dictionary<string, FurniturePosition> RoomFurnitureList;

    public RoomData() 
    {
        Id = "R" + Guid.NewGuid().ToString("N");
        RoomPos = new Vector2(0f, 0f);
        WallId = "";
        FloorId = "";
        RoomFurnitureList = new Dictionary<string, FurniturePosition>();
    }
}
