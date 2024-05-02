using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] GameObject Exterior;
    [SerializeField] GameObject Interior;
    [SerializeField] GameObject Decore;

    [SerializeField] GridSystem _gridSystem;

    public static int _saveObjectLastId;
    void Start()
    {
        Load();
    }

    /*
    public static void SaveObj(GameObject obj)
    {
        if (obj.GetComponent<Building>().SaveId != -1)
            Connection.SaveFurniture(obj.GetComponent<Building>().SaveId, obj.name.Replace("(Clone)", ""), obj.transform.position.x, obj.transform.position.y);
        else
        {
            _saveObjectLastId++;
            Debug.Log(_saveObjectLastId);
			Connection.SaveFurniture(_saveObjectLastId, obj.name.Replace("(Clone)", ""), obj.transform.position.x, obj.transform.position.y);
		}
	}*/

    public void Load()
    {
        List<SaveObj> list = Connection.GetSaveObjects();

        foreach (SaveObj obj in list)
        {
            GameObject prefab = Resources.Load("Prefabs/Furniture/" + Connection.GetPrefab(obj.Object_Id)) as GameObject;
            Furniture fur = Connection.GetObjectById(obj.Object_Id);
            int objLayer = _gridSystem.GetFurnitureLayer(Connection.GetType(obj.Object_Id));
            ParentType parentType = _gridSystem.GetObjectParentType(fur);
            GameObject parentObject;
            switch (parentType)
            {
                case ParentType.Exterior:
                    {
                        parentObject = Exterior;
                    }
                    break;
                case ParentType.Interior:
					{
						parentObject = Interior;
					}
					break;
                case ParentType.Decore:
					{
						parentObject = Decore;
					}
					break;
                default:
					{
						parentObject = Exterior;
					}
					break;
            }
            //GridBuildingSystem grid = _gridSystem.GetObjectGrid(fur);
            //grid.InitializeWithBuildingFromSave(prefab, new Vector2(obj.X, obj.Y), obj.Side);

            GameObject furniture = Instantiate(prefab, new Vector2(obj.X, obj.Y), Quaternion.identity, parentObject.transform);
			furniture.layer = objLayer;
            furniture.GetComponent<Building>().TurnSide(obj.Side);

			//if (_gridSystem.)
			/*
			switch (Connection.GetType(obj.Object_Id))
            {
                default:
                    break;
                case "exterior":
                    {
						GameObject furniture = Instantiate(prefab, new Vector2(obj.X, obj.Y), Quaternion.identity, Exterior.transform);
						furniture.layer = Exterior.layer;
					}
                    break;
				case "interior":
					{
						GameObject furniture = Instantiate(prefab, new Vector2(obj.X, obj.Y), Quaternion.identity, Interior.transform);
						furniture.layer = Interior.layer;
					}
					break;
				case "decore":
					{
						GameObject furniture = Instantiate(prefab, new Vector2(obj.X, obj.Y), Quaternion.identity, Decore.transform);
						furniture.layer = Decore.layer;
					}
					break;
			}*/
		}
    }

    public void SaveAll()
    {
        Debug.Log("Save all");

		GameObject[] gameObjectsHouses = GameObject.FindGameObjectsWithTag("HasInterior");
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Furniture");
        Connection.ClearFurnitureTable();
		foreach (GameObject obj in gameObjectsHouses)
		{
			Connection.Save(obj.name.Replace("(Clone)", ""), obj.transform.position.x, obj.transform.position.y, obj.GetComponent<Building>().Current_side);
		}
		foreach (GameObject obj in gameObjects)
        {
            Connection.Save(obj.name.Replace("(Clone)", ""), obj.transform.position.x, obj.transform.position.y, obj.GetComponent<Building>().Current_side);
        }
    }
}
