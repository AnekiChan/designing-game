using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Furniture> ExteriorFurnitures;
	[SerializeField] private List<Furniture> InteriorFurnitures;
	[SerializeField] private List<Furniture> Decore;

    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;

    public GridBuildingSystem currentGrid;

    public void OnEnable()
    {
        InteriorFurnitures = Connection.GetInterior();
        Decore = Connection.GetDecore();

        Render(InteriorFurnitures);
    }

    public void Render(List<Furniture> furnitures)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
		foreach (Furniture f in furnitures) Debug.Log(f.Title);
		foreach (Furniture furniture in furnitures)
        {
			var cell = Instantiate(_inventoryCellTemplate, _container);
			cell.Render(furniture, currentGrid);
		}
    }

    public void RenderInterior()
    {
        Render(InteriorFurnitures);
    }

    public void RenderExterior()
    {
        Render(ExteriorFurnitures);
    }

    public void RenderDecor()
    {
        Render(Decore);
    }
}

public enum GridType
{
    Environment,
    Interior,
    Decore
}
