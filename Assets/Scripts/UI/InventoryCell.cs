using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _iconField;
    public GameObject _furniturePref;

    public GridBuildingSystem _buildingSystem;
    
    public void Render(Furniture furniture, GridBuildingSystem grid)
    {
		//_furniturePref = furniture.Prefab;
		//_iconField.sprite = furniture.Prefab.GetComponent<Building>().Icon;
        
        _buildingSystem = grid;
    }

    public void OnButtonClick()
    {
        _buildingSystem.InitializeWithBuilding(_furniturePref);
    }
}
