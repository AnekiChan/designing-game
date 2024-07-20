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
	[SerializeField] GameObject WallGrid;
	[SerializeField] List<int> wallIds;

	private void Start()
	{
		ExteriorGrid.SetActive(false);
		InteriorGrid.SetActive(false);
		DecoreGrid.SetActive(false);
		WallGrid.SetActive(false);
	}
	private void OnEnable()
	{
		//Inventory.onEdited += Editing;
		EventBus.Instance.EditMode += Editing;
		GridBuildingSystem.onDestroyHouse += CreateInteriorGrid;
	}
	private void OnDisable()
	{
		EventBus.Instance.EditMode -= Editing;
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
			if (WallGrid != null)
			{
				WallGrid.SetActive(false);
			}
		}
	}
	/*
	public GridBuildingSystem GetObjectGrid(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return ExteriorGrid.GetComponent<GridBuildingSystem>();
		else if (interiorIds.Contains(furniture.Type)) return InteriorGrid.GetComponent<GridBuildingSystem>();
		else if (wallIds.Contains(furniture.Type)) return WallGrid.GetComponent<GridBuildingSystem>();
		else return DecoreGrid.GetComponent<GridBuildingSystem>();
	}

	public ParentType GetObjectParentType(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return ParentType.Exterior;
		else if (interiorIds.Contains(furniture.Type)) return ParentType.Interior;
		else if (wallIds.Contains(furniture.Type)) return ParentType.Walls;
		else return ParentType.Decore;
	}
	*/
	public int GetFurnitureLayer(int typeId)
	{
		if (exteriorIds.Contains(typeId)) return ExteriorGrid.layer;
		else if (interiorIds.Contains(typeId)) return InteriorGrid.layer;
		else if (wallIds.Contains(typeId)) return WallGrid.layer;
		else return DecoreGrid.layer;
	}

	public void CreateInteriorGrid()
	{
		InteriorGrid.transform.Rotate(new Vector3(0, 0, 0));
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

	public void CreateWallGrid()
	{
		WallGrid.GetComponent<GridBuildingSystem>().ClearMainArea();

		GameObject[] houses = GameObject.FindGameObjectsWithTag("HasInterior");
		foreach (GameObject house in houses)
		{
			BoundsInt prevArea = house.GetComponent<Building>().area;
			prevArea.position = new Vector3Int(Convert.ToInt32(house.transform.position.x), Convert.ToInt32(house.transform.position.y), Convert.ToInt32(house.transform.position.z));

			WallGrid.GetComponent<GridBuildingSystem>()?.CreateMainArea(prevArea, house.transform);
		}
	}

	public void CreateLeftWall()
	{
		InteriorGrid.transform.Rotate(new Vector3(0, 0, 60));
		InteriorGrid.GetComponent<GridBuildingSystem>().ClearMainArea();

		GameObject[] houses = GameObject.FindGameObjectsWithTag("LeftWall");
		
		foreach(var house in houses)
		{
			Tilemap tilemap = house.GetComponent<Tilemap>();
			InteriorGrid.GetComponent<GridBuildingSystem>().SetTilesByPosition(tilemap, house);
		}
	}

	public void CreateRightWall()
	{
		InteriorGrid.transform.Rotate(new Vector3(0, 0, -60));
	}
}

public enum ParentType
{
	Exterior,
	Interior,
	Decore,
	Walls
}
