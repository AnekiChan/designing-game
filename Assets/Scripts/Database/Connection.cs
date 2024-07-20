using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public static class Connection
{
	private static string path = Application.dataPath + "/AssetsDatabase/db.bytes";
    public static SqliteConnection DbConnection = new SqliteConnection("URI=file:" + path);

    public static void SetConnection()
    {
        DbConnection.Open();
        if (DbConnection.State == ConnectionState.Open)
        {
            //Debug.Log("DB open");
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = DbConnection;
            cmd.CommandText = "SELECT * FROM objects";
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));
            }
        }
        else
        {
            Debug.Log("Error connection");
        }
        DbConnection.Close();
    }

	public static Furniture GetObjectById(int id)
	{
		Furniture furniture = null;
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = $"SELECT * FROM furniture WHERE id={id}";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					GameObject obj = Resources.Load("Prefabs/Furniture/" + reader[2].ToString()) as GameObject;
					if (obj == null) Debug.Log("null " + reader[2].ToString());

					//furniture = new Furniture(Int32.Parse(reader[0].ToString()), reader[1].ToString(), obj, Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()));
					//Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));

				}

				DbConnection.Close();
				return furniture;
			}
			catch (Exception e)
			{
				Debug.LogException(e);

				DbConnection.Close();
				return null;
			}
		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return null;
		}
	}

	public static Furniture GetObjectByPrefab(string prefab)
	{
		Furniture furniture = null;
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{

			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = $"SELECT * FROM furniture WHERE prefab='{prefab}'";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					GameObject obj = Resources.Load("Prefabs/Furniture/" + reader[2].ToString()) as GameObject;
					if (obj == null) Debug.Log("null " + reader[2].ToString());

					//furniture = new Furniture(Int32.Parse(reader[0].ToString()), reader[1].ToString(), obj, Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()));
					//Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));

				}

				DbConnection.Close();
				return furniture;
			}
			catch (Exception e)
			{
				Debug.LogException(e);

				DbConnection.Close();
				return null;
			}
		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return null;
		}
	}

	public static List<Furniture> GetObjects(string command)
    {
        List<Furniture> furnitures = new List<Furniture>();

		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			try
            {
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = command;
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
                    GameObject obj = Resources.Load("Prefabs/Furniture/" + reader[2].ToString()) as GameObject;
                    if (obj == null) Debug.Log("null " + reader[2].ToString());

					//furnitures.Add(new Furniture(Int32.Parse(reader[0].ToString()), reader[1].ToString(), obj, Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString())));
				}
				
				DbConnection.Close();
				return furnitures;
			}
            catch (Exception e)
            {
                Debug.LogException(e);

				DbConnection.Close();
				return null;
			}
			
		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
            return null;
		}
	}
	public static List<Furniture> GetHouses() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='house')");
	public static List<Furniture> GetPlants() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='plants')");
	public static List<Furniture> GetOutdors() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='outdoors')");
	public static List<Furniture> GetBigFurniture() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='bigfurniture')");
	public static List<Furniture> GetSmallFurniture() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='smallfurniture')");
	public static List<Furniture> GetDecore() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='decore')");
	public static List<Furniture> GetWall() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='wall')");
	public static List<Furniture> GetCarpet() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='carpet')");

	public static List<CreatureScriptableObject> GetCreatures()
	{
		List<CreatureScriptableObject> creatures = new List<CreatureScriptableObject>();

		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			//Debug.Log("DB open");
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = "SELECT * FROM creatures";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					CreatureScriptableObject obj = Resources.Load("Prefabs/Creatures/ScriptableObjects/" + reader[2].ToString()) as CreatureScriptableObject;
					if (obj == null) Debug.Log("null " + reader[2].ToString());

					creatures.Add(obj);
					//Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));

				}

				DbConnection.Close();
				return creatures;
			}
			catch (Exception e)
			{
				Debug.LogException(e);

				DbConnection.Close();
				return null;
			}

		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return null;
		}
	}

	public static void Save(string pref, float x, float y, int side)
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			//Debug.Log("DB open for save");
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;

				cmd.CommandText = $"INSERT INTO objects_save (object_id, x_pos, y_pos, side) VALUES ((SELECT id FROM furniture WHERE prefab='{pref}'),@x,@y,{side})";
				SqliteParameter xPar = new SqliteParameter("@x", x);
				cmd.Parameters.Add(xPar);
				SqliteParameter yPar = new SqliteParameter("@y", y);
				cmd.Parameters.Add(yPar);
				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}

		}
		else
		{
			Debug.Log("Error connection");
		}
		DbConnection.Close();
	}

	public static void ClearSaveTable()
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				// отчиска таблицы для перезаписи
				cmd.CommandText = "DELETE FROM objects_save";
				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
		else
		{
			Debug.Log("Error connection");
		}
		DbConnection.Close();
	}
	/*
	public static List<SaveObj> GetSaveObjects()
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			List<SaveObj> saveObjects = new List<SaveObj>();
			//Debug.Log("DB open for save");
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = "SELECT * FROM objects_save";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					saveObjects.Add(new SaveObj(Int32.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()), float.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString())));
				}
				DbConnection.Close();
				return saveObjects;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				DbConnection.Close();
				return null;
			}

		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return null;
		}
	}
	*/
	public static string GetPrefab(int id)
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			try
			{
				string pref = "";
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = $"SELECT prefab FROM furniture WHERE id={id}";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					pref = reader[0].ToString();
				}
				DbConnection.Close();
				return pref;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				DbConnection.Close();
				return "";
			}

		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return "";
		}
	}

	public static int GetType(int objId)
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			try
			{
				string typeTitle = "";
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = $"SELECT type FROM furniture WHERE id={objId}";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					typeTitle = reader[0].ToString();
				}
				DbConnection.Close();
				return Int32.Parse(typeTitle);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				DbConnection.Close();
				return 0;
			}

		}
		else
		{
			Debug.Log("Error connection");
			DbConnection.Close();
			return 0;
		}
	}

	public static void ClearSave()
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			//Debug.Log("DB open for save");
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;

				cmd.CommandText = $"DELETE FROM objects_save";
				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}

		}
		else
		{
			Debug.Log("Error connection");
		}
		DbConnection.Close();
	}
}
