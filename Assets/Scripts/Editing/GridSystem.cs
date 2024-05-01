using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
	[SerializeField] GameObject Exterior;
	[SerializeField] List<int> exteriorIds;
	[SerializeField] GameObject Interior;
	[SerializeField] List<int> interiorIds;
	[SerializeField] GameObject Decore;
	[SerializeField] List<int> decoreIds;

	private void Start()
	{
		Interior.SetActive(false);
		Decore.SetActive(false);
	}

	public GridBuildingSystem GetObjectGrid(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return Exterior.GetComponent<GridBuildingSystem>();
		else if (interiorIds.Contains(furniture.Type)) return Interior.GetComponent<GridBuildingSystem>();
		else return Decore.GetComponent<GridBuildingSystem>();
	}

	public ParentType GetObjectParentType(Furniture furniture)
	{
		if (exteriorIds.Contains(furniture.Type)) return ParentType.Exterior;
		else if (interiorIds.Contains(furniture.Type)) return ParentType.Interior;
		else return ParentType.Decore;
	}

	public int GetFurnitureLayer(int id)
	{
		if (exteriorIds.Contains(id)) return Exterior.layer;
		else if (interiorIds.Contains(id)) return Interior.layer;
		else return Decore.layer;
	}

	public void CreateInteriorGrid()
	{
		GameObject[] houses = GameObject.FindGameObjectsWithTag("HasInterior");
		foreach (GameObject house in houses)
		{
			BoundsInt prevArea = house.GetComponent<Building>().area;
			Interior.GetComponent<GridBuildingSystem>()?.CreateMainArea(prevArea);
		}
	}
}

public enum ParentType
{
	Exterior,
	Interior,
	Decore
}
