using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEditedGrid : MonoBehaviour
{
    public GameObject Environmentgrid;
    public GameObject DecoreGrid;

    public void ChangeToEnvironmentGrid()
    {
        DecoreGrid.SetActive(false);
        Environmentgrid.SetActive(true);
    }

    public void ChangeToDecoretGrid()
    {
        Environmentgrid.SetActive(false);
        DecoreGrid.SetActive(true);
    }
}
