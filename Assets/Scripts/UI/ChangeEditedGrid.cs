using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEditedGrid : MonoBehaviour
{
    public GameObject EnvironmentGrid;
    public GameObject DecoreGrid;

    public void ChangeToEnvironmentGrid()
    {
        GetComponent<Inventory>().currentGrid = EnvironmentGrid.GetComponent<GridBuildingSystem>();
        DecoreGrid.SetActive(false);
        EnvironmentGrid.SetActive(true);
    }

    public void ChangeToDecoretGrid()
    {
		GetComponent<Inventory>().currentGrid = DecoreGrid.GetComponent<GridBuildingSystem>();
		EnvironmentGrid.SetActive(false);
        DecoreGrid.SetActive(true);
    }
}
