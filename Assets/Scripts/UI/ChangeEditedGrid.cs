using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEditedGrid : MonoBehaviour
{
    public GameObject EnvironmentGrid;
	public GameObject InteriorGrid;
	public GameObject DecoreGrid;

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
	}

	public void ChangeToInteriorGrid()
	{
		GetComponent<Inventory>().currentGrid = InteriorGrid.GetComponent<GridBuildingSystem>();
		DecoreGrid.SetActive(false);
		EnvironmentGrid.SetActive(false);
		InteriorGrid.SetActive(true);
	}

	public void ChangeToDecoretGrid()
    {
		GetComponent<Inventory>().currentGrid = DecoreGrid.GetComponent<GridBuildingSystem>();
		EnvironmentGrid.SetActive(false);
        DecoreGrid.SetActive(true);
		InteriorGrid.SetActive(false);
	}
}
