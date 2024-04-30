using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] public Sprite Icon;
    public bool Placed {  get; private set; }
    public BoundsInt area;

    public List<GameObject> sprites = new List<GameObject>();
    public List <Collider2D> colliders = new List<Collider2D>();
    private int side_count;
    public int Current_side { get; private set; } = 0;

    public bool iSOccupied = false;
    public bool isSitting = false;
    public Transform SeatPos;


	private void Awake()
    {
        if (SeatPos == null) SeatPos = transform;

        if (sprites.Count == colliders.Count)
        {
            side_count = sprites.Count;
        }
        else
            throw new Exception(" оличество спрайтов и коллайдеров не совпадает");

        for (int i = 1; i < side_count; i++)
        {
            sprites[i].SetActive(false);
            colliders[i].enabled = false;
        }
    }

    public void TurnSide()
    {
        sprites[Current_side].SetActive(false);
        colliders[Current_side].enabled = false;
        Current_side = (Current_side + 1) % side_count;
        sprites[Current_side].SetActive(true);
        colliders[Current_side].enabled = true;

        int x = area.size.x;
        int y = area.size.y;

        area.size = new Vector3Int(y, x, 1);
    }

	public void TurnSide(int side)
	{
		while (Current_side != side)
        {
            
			sprites[Current_side].SetActive(false);
			colliders[Current_side].enabled = false;
			Current_side = (Current_side + 1) % side_count;
			sprites[Current_side].SetActive(true);
			colliders[Current_side].enabled = true;

			int x = area.size.x;
			int y = area.size.y;

			area.size = new Vector3Int(y, x, 1);
		}
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
