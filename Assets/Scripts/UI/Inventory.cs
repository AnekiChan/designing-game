using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<FurnitureScriptbleObject> ExteriorFurnitures;
    [SerializeField] private List<FurnitureScriptbleObject> Decor;

    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;

    public GridBuildingSystem currentGrid;

    public void OnEnable()
    {
        Render(ExteriorFurnitures);
    }

    public void Render(List<FurnitureScriptbleObject> furnitures)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        furnitures.ForEach(furniture =>
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Render(furniture, currentGrid);
        });
    }

    public void RenderInterior()
    {
        //Render(InteriorFurnitures);
    }

    public void RenderExterior()
    {
        Render(ExteriorFurnitures);
    }

    public void RenderDecor()
    {
        Render(Decor);
    }
}

public enum GridType
{
    Environment,
    Interior,
    Decore
}
