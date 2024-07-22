using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using System;
using FurnitureTypesList;

public class GridBuildingSystem : MonoBehaviour
{
	//[SerializeField] private Inventory EditPanel;

    public GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    [SerializeField] private Tile whiteTile;
    [SerializeField] private Tile greenTile;
    [SerializeField] private Tile redTile;

    public Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp;
    private Vector3 prevPos;
    private Transform _parent;
    public int LayerNumber;

    private BoundsInt prevArea;

    public bool isMoving = false;

	#region Unity Methods

	private void Awake()
    {
        current = this;
        _parent = transform.parent;
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, whiteTile);
        tileBases.Add(TileType.Green, greenTile);
        tileBases.Add(TileType.Red, redTile);

        ActiveMaintilemap(false);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // �����������
        if (Input.GetMouseButtonDown(0))
        {
            if (temp != null)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    ActiveMaintilemap(true);
				}
                else
                {
                    if (temp.CanBePlaced(current))
                    {
                        isMoving = false;
                        temp.Place(current);
                        temp = null;
                        //EditPanel.HideInvantory(false);
						ActiveMaintilemap(false);
					}
                }
            }
            else
            {
                MoveObject();
			}
            
        }

        if (isMoving)
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                RemoveObject();

			}
            else if (Input.GetMouseButtonDown(1))
            {
                RotateObject();
			}

            Vector2 touchPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            if (prevPos != (Vector3)cellPos)
            {
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated((Vector3)cellPos + new Vector3(0.5f, 0.5f, 0f));
                prevPos = (Vector3)cellPos;
                FollowBuilding();
            }

        }
	}

    private void MoveObject()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		int mask = 1 << LayerNumber;
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 2, mask);

        foreach (var hit in hits)
        {
			if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject() && hit.collider.tag == "Furniture")
			{
				//Debug.Log("CLICKED " + hit.collider.name);
				ClearPrev(hit.collider.gameObject.GetComponent<Building>());
				temp = hit.collider.gameObject.GetComponent<Building>();
				//EditPanel.HideInvantory(true);
				isMoving = true;
				FollowBuilding();
				ActiveMaintilemap(true);
			}
		}
	}

    private void RemoveObject()
    {
		ClearTempArea();
		// delete sql
		Destroy(temp.gameObject);
		isMoving = false;
		temp = null;
		//EditPanel.HideInvantory(false);
	}

    private void RotateObject()
    {
		ClearTempArea();
		prevArea = temp.area;
		temp.TurnSide();
		FollowBuilding();
	}

	private void ActiveMaintilemap(bool b)
    {
		//MainTilemap.gameObject.SetActive(b);
        if (b)
        {
            MainTilemap.gameObject.SetActive(true);
        }
        else
        {
            MainTilemap.gameObject.SetActive(false);
		}
	}

    #endregion

    #region Tilemap Management

    private TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    public void SetTilesByPosition(Tilemap tilemap, GameObject house)
    {
		BoundsInt bounds = tilemap.cellBounds;
		bounds.position = new Vector3Int(Convert.ToInt32(house.transform.position.x), Convert.ToInt32(house.transform.position.y), Convert.ToInt32(house.transform.position.z));
		foreach (var position in bounds.allPositionsWithin)
		{
			// �������� ���� � ��������� �������
			TileBase tile = tilemap.GetTile(position);

			// ���������, ����� �� ���� ����, �������� �� ����
			if (tile == whiteTile)
			{
				// ���� ��, ������� ���������� � �����
				MainTilemap.SetTile(position, whiteTile);
			}
		}
	}

    private void FillTiles(TileBase[] array, TileType type)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tileBases[type];
        }
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach(var b in baseArray)
        {
            if (b!= tileBases[TileType.White])
            {
                //Debug.Log("Can't place here");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Green, MainTilemap);
    }

    public void CreateMainArea(BoundsInt area, Transform pos)
    {
		int size = area.size.x * area.size.y * area.size.z;
		area.position = gridLayout.WorldToCell(pos.position);
		TileBase[] tileArray = new TileBase[size];
		FillTiles(tileArray, TileType.White);
		MainTilemap.SetTilesBlock(area, tileArray);
	}

    #endregion

    #region Building Placement

    public void InitializeWithBuilding(FurnitureSO building)
    {
        if (building.FurnitureType != FurnitureType.Floor && building.FurnitureType != FurnitureType.Wall)
        {
            GameObject obj = Instantiate(building.Prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, _parent);
            obj.layer = gameObject.layer;
            temp = obj.GetComponent<Building>();
            //EventBus.Instance.ChangeScore?.Invoke(Connection.GetObjectByPrefab(building.name).Score);

            ActiveMaintilemap(true);
            isMoving = true;
            FollowBuilding();
            //EditPanel.HideInvantory(true);
        }
    }

    /*
	public void InitializeWithBuildingFromSave(GameObject building, Vector2 pos, int side)
	{
		GameObject obj = Instantiate(building, pos, Quaternion.identity, _parent);
		obj.layer = gameObject.layer;
		obj.GetComponent<Building>().TurnSide(side);
	}*/

	private void ClearTempArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

	public void ClearMainArea()
	{
		BoundsInt bounds = MainTilemap.cellBounds;
		TileBase[] allTiles = MainTilemap.GetTilesBlock(bounds);

		foreach (var position in bounds.allPositionsWithin)
		{
			if (MainTilemap.HasTile(position))
			{
				MainTilemap.SetTile(position, tileBases[TileType.Empty]);
			}
		}
	}

	private void FollowBuilding()
    {
        ClearTempArea();
        
        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    private void ClearPrev(Building prevTemp)
    {
        ClearTempArea();

        prevTemp.area.position = gridLayout.WorldToCell(prevTemp.gameObject.transform.position);
        BoundsInt buildingArea = prevTemp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;

        for (int i = 0; i < baseArray.Length; i++)
        {
            baseArray[i] = tileBases[TileType.White];
        }

        MainTilemap.SetTilesBlock(buildingArea, baseArray);
    }

    public void ClearCurrentTemp()
    {
        temp = null;
    }

    #endregion
}

public enum TileType
{
    Empty,
    White,
    Green,
    Red
}