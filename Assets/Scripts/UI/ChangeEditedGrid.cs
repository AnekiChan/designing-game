using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEditedGrid : MonoBehaviour
{
    public GameObject EnvironmentGrid;
	public GameObject InteriorGrid;
	public GameObject DecoreGrid;
	public GameObject WallGrid;

	private void Start()
	{
		GetComponent<Inventory>().currentGrid = EnvironmentGrid.GetComponent<GridBuildingSystem>();
	}

	public void ChangeToEnvironmentGrid()
    {
        GetComponent<Inventory>().currentGrid = EnvironmentGrid.GetComponent<GridBuildingSystem>();
        DecoreGrid.SetActive(false);
        EnvironmentGrid.SetActive(true);
		InteriorGrid.SetActive(false);
		WallGrid.SetActive(false);
	}

	public void ChangeToInteriorGrid()
	{
		GetComponent<Inventory>().currentGrid = InteriorGrid.GetComponent<GridBuildingSystem>();
		DecoreGrid.SetActive(false);
		EnvironmentGrid.SetActive(false);
		InteriorGrid.SetActive(true);
		WallGrid.SetActive(false);
	}

	public void ChangeToDecoretGrid()
    {
		GetComponent<Inventory>().currentGrid = DecoreGrid.GetComponent<GridBuildingSystem>();
		EnvironmentGrid.SetActive(false);
        DecoreGrid.SetActive(true);
		InteriorGrid.SetActive(false);
		WallGrid.SetActive(false);
	}

	public void ChangeToWallrGrid()
	{
		GetComponent<Inventory>().currentGrid = WallGrid.GetComponent<GridBuildingSystem>();
		DecoreGrid.SetActive(false);
		EnvironmentGrid.SetActive(false);
		InteriorGrid.SetActive(false);
		WallGrid.SetActive(true);
	}
}
