using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FurnitureTypesList;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _iconField;
    //public GameObject _furniturePref;
    private FurnitureSO _furniture;

    private GridSystem _gridSystem;
    public GridBuildingSystem _buildingSystem;

    public void Render(FurnitureSO furniture, GridSystem grid)
    {
		_furniture = furniture;
        _iconField.sprite = furniture.UIIcon;
        _gridSystem = grid;
    }

    public void OnButtonClick()
    {
        if (_furniture.FurnitureType == FurnitureType.Wall)
        {
            EventBus.Instance.ChangeWall.Invoke(_furniture);
        }
        else if (_furniture.FurnitureType == FurnitureType.Floor)
        {
            EventBus.Instance.ChangeFloor.Invoke(_furniture);
        }
        else
        {
            EventBus.Instance.ChangeGrid(_furniture);
            _buildingSystem = _gridSystem.GetObjectGrid(_furniture);
            _buildingSystem.InitializeWithBuilding(_furniture);
        }
    }
}
