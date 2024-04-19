using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] GameObject Exterior;
    [SerializeField] GameObject Interior;
    [SerializeField] GameObject Decore;

    public static int _saveObjectLastId;
    void Start()
    {
        Load();
    }

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
	}

    public void Load()
    {
        List<SaveObj> list = Connection.GetSaveObjects();

        foreach (SaveObj obj in list)
        {
            GameObject prefab = Resources.Load("Prefabs/Furniture/" + Connection.GetPrefab(obj.Object_Id)) as GameObject;
            prefab.GetComponent<Building>().SaveId = obj.Id;
            _saveObjectLastId = obj.Id;

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
			}
		}
    }
}
