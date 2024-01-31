using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    [SerializeField] private Tile whiteTile;
    [SerializeField] private Tile greenTile;
    [SerializeField] private Tile redTile;

    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp;
    private Vector3 prevPos;

    private BoundsInt prevArea;

    public bool isMoving = false;

    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, whiteTile);
        tileBases.Add(TileType.Green, greenTile);
        tileBases.Add(TileType.Red, redTile);
    }

    private void Update()
    {
        // расстановка
        if (Input.GetMouseButtonDown(0))
        {
            if (temp != null)
            {
                if (!isMoving)
                {
                    isMoving = true;
                }
                else
                {
                    if (temp.CanBePlaced())
                    {
                        isMoving = false;
                        temp.Place();
                        temp = null;
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    //Debug.Log("CLICKED " + hit.collider.name);
                    ClearPrev(hit.collider.gameObject.GetComponent<Building>());
                    temp = hit.collider.gameObject.GetComponent<Building>();
                    isMoving = true;
                    FollowBuilding();
                }
            }
            
        }
        

        if (isMoving)
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ClearArea();
                Destroy(temp.gameObject);
                isMoving = false;
                temp = null;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ClearArea();
                prevArea = temp.area;
                temp.TurnSide();
                FollowBuilding();
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

    #endregion

    #region Tilemap Management

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
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

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] array, TileType type)
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
                Debug.Log("Can't place here");
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

    #endregion

    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Building>();
        isMoving = true;
        FollowBuilding();
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();

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
        ClearArea();

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

    #endregion
}

public enum TileType
{
    Empty,
    White,
    Green,
    Red
}