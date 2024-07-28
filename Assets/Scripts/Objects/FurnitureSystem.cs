using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FurnitureTypesList;

public class FurnitureSystem: MonoBehaviour, IDataPersistance
{
    private static List<FurnitureSO> _allFurniture;
    public static List<FurnitureSO> AllFurniture => _allFurniture;
    [SerializeField] GameObject Interior;
    [SerializeField] GameObject Decore;
    [SerializeField] GameObject WallDecore;
    private FurnitureSO Wall;
    [SerializeField] FurnitureSO _defaultWallSO;
    private FurnitureSO Floor;
    [SerializeField] FurnitureSO _defaultFloorSO;

    private void Awake()
    {
        _allFurniture = new List<FurnitureSO>(Resources.LoadAll<FurnitureSO>("Prefabs/Furniture/SO"));
        Wall = _defaultWallSO;
        Floor = _defaultFloorSO;
    }

    private void OnEnable()
    {
        EventBus.Instance.ChangeFloor += ChangeFloor;
        EventBus.Instance.ChangeWall += ChangeWall;
    }

    private void OnDisable()
    {
        EventBus.Instance.ChangeWall -= ChangeWall;
        EventBus.Instance.ChangeFloor -= ChangeFloor;
    }

    private void ChangeFloor(FurnitureSO floor)
    {
        Floor = floor;
    }

    private void ChangeWall(FurnitureSO wall)
    {
        Wall = wall;
    }

    // загрузка комнаты из сохранения
    public void LoadData(GameData data)
    {
        EventBus.Instance.ChangeFloor(_allFurniture.Find(p => p.Id == data.RoomDataList[0].FloorId));
        EventBus.Instance.ChangeWall(_allFurniture.Find(p => p.Id == data.RoomDataList[0].WallId));

        foreach (KeyValuePair<string, FurniturePosition> furniture in data.RoomDataList[0].RoomFurnitureList)
        {
            FurnitureSO furnitureSO = _allFurniture.Find(p => p.Id == furniture.Key);
            GameObject obj;
            if (furnitureSO.FurnitureType == FurnitureType.Decor)
            {
                obj = Instantiate(furnitureSO.Prefab, new Vector2(furniture.Value._XPos, furniture.Value._YPos), Quaternion.identity, Interior.transform);
            }
            else if (furnitureSO.FurnitureType == FurnitureType.WallDecor)
            {
                obj = Instantiate(furnitureSO.Prefab, new Vector2(furniture.Value._XPos, furniture.Value._YPos), Quaternion.identity, WallDecore.transform);
            }
            else
            {
                obj = Instantiate(furnitureSO.Prefab, new Vector2(furniture.Value._XPos, furniture.Value._YPos), Quaternion.identity, Interior.transform);
            }
            
            obj.layer = obj.transform.parent.gameObject.layer;
            obj.GetComponent<Building>().TurnSide(furniture.Value.Rotation);
        }
    }

    // сохранение комнаты
    public void SaveData(ref GameData data)
    {
        RoomData roomData = new RoomData();
        for (int i = 0; i < Interior.transform.childCount; i++)
        {
            GameObject obj = Interior.transform.GetChild(i).gameObject;
            if (obj != null && obj.GetComponent<Furniture>() != null)
            {
                Building building = obj.GetComponent<Building>();
                roomData.RoomFurnitureList.Add(obj.GetComponent<Furniture>().FurnitureSO.Id, new FurniturePosition(building.area.x, building.area.y, building.Current_side));
            }
        }
        roomData.FloorId = Floor.Name;
        roomData.WallId = Wall.Name;

        data.RoomDataList.Add(roomData);
    }
}
