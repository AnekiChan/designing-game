using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed {  get; private set; }
    public BoundsInt area;

    public List<GameObject> sprites = new List<GameObject>();
    public List <Collider2D> colliders = new List<Collider2D>();
    private int side_count;
    private int current_side = 0;

    private void Start()
    {
        if (sprites.Count == colliders.Count)
        {
            side_count = sprites.Count;
        }
        else
            throw new Exception("���������� �������� � ����������� �� ���������");

        for (int i = 1; i < side_count; i++)
        {
            sprites[i].SetActive(false);
            colliders[i].enabled = false;
        }
    }

    public void TurnSide()
    {
        sprites[current_side].SetActive(false);
        colliders[current_side].enabled = false;
        current_side = (current_side + 1) % side_count;
        sprites[current_side].SetActive(true);
        colliders[current_side].enabled = true;

        int x = area.size.x;
        int y = area.size.y;

        area.size = new Vector3Int(y, x, 1);
    }

    #region Build Methods

    public bool CanBePlaced(GridBuildingSystem grid)
    {
        Vector3Int positionInt = grid.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (grid.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place(GridBuildingSystem grid)
    {
        Vector3Int positionInt = grid.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        grid.current.TakeArea(areaTemp);
    }

    #endregion
}