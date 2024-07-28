using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<RoomData> RoomDataList;

    public GameData()
    {
        this.RoomDataList = new List<RoomData>();
    }
}
