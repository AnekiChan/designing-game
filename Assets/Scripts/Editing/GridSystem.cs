using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem: MonoBehaviour
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
}
