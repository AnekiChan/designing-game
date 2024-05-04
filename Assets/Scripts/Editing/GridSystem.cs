using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
	[SerializeField] GameObject ExteriorGrid;
	[SerializeField] List<int> exteriorIds;
	[SerializeField] GameObject InteriorGrid;
	[SerializeField] List<int> interiorIds;
	[SerializeField] GameObject DecoreGrid;
	[SerializeField] List<int> decoreIds;

	private void Start()
	{
		ExteriorGrid.SetActive(false);
		InteriorGrid.SetActive(false);
		DecoreGrid.SetActive(false);
	}
	private void OnEnable()
	{
		Inventory.onEdited += Editing;
		GridBuildingSystem.onDestroyHouse += CreateInteriorGrid;
	}
	private void OnDisable()
	{
		Inventory.onEdited -= Editing;
		GridBuildingSystem.onDestroyHouse -= CreateInteriorGrid;
	}

	private void OnDestroy()
	{
		Inventory.onEdited -= Editing;
		GridBuildingSystem.onDestroyHouse -= CreateInteriorGrid;
	}

	private void Editing(bool isEditing)
	{
		if (isEditing)
		{
			if (ExteriorGrid != null)
			{
				ExteriorGrid.SetActive(true);
			}
		}
		else
		{
			if (ExteriorGrid != null)
			{
				ExteriorGrid.SetActive(false);
			}
			if (InteriorGrid != null)
			{
				InteriorGrid.SetActive(false);
			}
			if (DecoreGrid != null)
			{
				DecoreGrid.SetActive(false);
			}
		}
	}

	public GridBuildingSystem GetObjectGrid(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return ExteriorGrid.GetComponent<GridBuildingSystem>();
		else if (interiorIds.Contains(furniture.Type)) return InteriorGrid.GetComponent<GridBuildingSystem>();
		else return DecoreGrid.GetComponent<GridBuildingSystem>();
	}

	public ParentType GetObjectParentType(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return ParentType.Exterior;
		else if (interiorIds.Contains(furniture.Type)) return ParentType.Interior;
		else return ParentType.Decore;
	}

	public int GetFurnitureLayer(int typeId)
	{
		if (exteriorIds.Contains(typeId)) return ExteriorGrid.layer;
		else if (interiorIds.Contains(typeId)) return InteriorGrid.layer;
		else return DecoreGrid.layer;
	}

	public void CreateInteriorGrid()
	{
		InteriorGrid.GetComponent<GridBuildingSystem>().ClearMainArea();

		GameObject[] houses = GameObject.FindGameObjectsWithTag("HasInterior");
		foreach (GameObject house in houses)
		{
			BoundsInt prevArea = house.GetComponent<Building>().area;
			prevArea.position = new Vector3Int(Convert.ToInt32(house.transform.position.x), Convert.ToInt32(house.transform.position.y), Convert.ToInt32(house.transform.position.z));
			
			InteriorGrid.GetComponent<GridBuildingSystem>()?.CreateMainArea(prevArea, house.transform);
		}
	}

	public void CreateDecoreGrid()
	{
		DecoreGrid.GetComponent<GridBuildingSystem>().ClearMainArea();

		GameObject[] houses = GameObject.FindGameObjectsWithTag("HasInterior");
		foreach (GameObject house in houses)
		{
			BoundsInt houseArea = house.GetComponent<Building>().area;
			BoundsInt prevArea = new BoundsInt();
			prevArea.size = new Vector3Int(houseArea.size.x * 2-1, houseArea.size.y * 2-1, houseArea.size.z);
			prevArea.position = new Vector3Int(Convert.ToInt32(house.transform.position.x), Convert.ToInt32(house.transform.position.y), Convert.ToInt32(house.transform.position.z));
			
			DecoreGrid.GetComponent<GridBuildingSystem>()?.CreateMainArea(prevArea, house.transform);
		}
	}
}

public enum ParentType
{
	Exterior,
	Interior,
	Decore
}
