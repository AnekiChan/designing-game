using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _iconField;
    public GameObject _furniturePref;

    public GridBuildingSystem _buildingSystem;
    
    public void Render(IFurniture furniture, GridBuildingSystem grid)
    {
        _iconField.sprite = furniture.UIIcon;
        _furniturePref = furniture.Pref;
        _buildingSystem = grid;
    }

    public void OnButtonClick()
    {
        _buildingSystem.InitializeWithBuilding(_furniturePref);
    }
}
