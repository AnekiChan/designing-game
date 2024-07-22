using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FurnitureTypesList;

public class GridSystem : MonoBehaviour
{
	[SerializeField] GameObject InteriorGrid;
	[SerializeField] GameObject DecoreGrid;
	[SerializeField] GameObject WallGrid;

    private void Start()
	{
		InteriorGrid.SetActive(false);
		DecoreGrid.SetActive(false);
		WallGrid.SetActive(false);
	}
	private void OnEnable()
	{
		//Inventory.onEdited += Editing;
		//EventBus.Instance.EditMode += Editing;
		EventBus.Instance.ChangeGrid += ActiveGrid;
	}
	private void OnDisable()
	{
        //EventBus.Instance.EditMode -= Editing;
        EventBus.Instance.ChangeGrid -= ActiveGrid;
    }

	private void Editing(bool isEditing)
	{
		if (isEditing)
		{
			if (InteriorGrid != null)
			{
                InteriorGrid.SetActive(true);
			}
		}
		else
		{
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

	public void ActiveGrid(FurnitureSO furniture)
	{
        if (furniture.FurnitureType == FurnitureType.Decor)
		{
			InteriorGrid.SetActive(false);
			DecoreGrid.SetActive(true);
			WallGrid.SetActive(false);
        }
        else if (furniture.FurnitureType == FurnitureType.WallDecor)
        {
            InteriorGrid.SetActive(false);
            DecoreGrid.SetActive(false);
            WallGrid.SetActive(true);
        }
        else
        {
            InteriorGrid.SetActive(true);
            DecoreGrid.SetActive(false);
            WallGrid.SetActive(false);
        }
    }
	
	public GridBuildingSystem GetObjectGrid(FurnitureSO furniture)
	{
		if (furniture.FurnitureType == FurnitureType.Decor) return DecoreGrid.GetComponent<GridBuildingSystem>();
		else if (furniture.FurnitureType == FurnitureType.WallDecor) return WallGrid.GetComponent<GridBuildingSystem>();
		else return InteriorGrid.GetComponent<GridBuildingSystem>();
	}
	
	public ParentType GetObjectParentType(FurnitureSO furniture)
	{
        if (furniture.FurnitureType == FurnitureType.Decor) return ParentType.Decore;
        else if (furniture.FurnitureType == FurnitureType.WallDecor) return ParentType.Walls;
        else return ParentType.Interior;
    }
	
	public int GetFurnitureLayer(FurnitureSO furniture)
	{
        if (furniture.FurnitureType == FurnitureType.Decor) return DecoreGrid.layer;
        else if (furniture.FurnitureType == FurnitureType.WallDecor) return WallGrid.layer;
        else return InteriorGrid.layer;
    }

	/*
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
			prevArea.size = new Vector3Int(houseArea.size.x * 2 - 1, houseArea.size.y * 2 - 1, houseArea.size.z);
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
	*/

}

public enum ParentType
{
	Interior,
	Decore,
	Walls
}
