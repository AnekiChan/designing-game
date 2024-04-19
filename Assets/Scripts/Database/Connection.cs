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
            Debug.Log("DB open");
            /*
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = DbConnection;
            cmd.CommandText = "SELECT * FROM objects";
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));
            }
            */
        }
        else
        {
            Debug.Log("Error connection");
        }
        DbConnection.Close();
    }

    public static List<Furniture> GetObjects(string command)
    {
        List<Furniture> furnitures = new List<Furniture>();

		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			Debug.Log("DB open");
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

					furnitures.Add(new Furniture(Int32.Parse(reader[0].ToString()), reader[1].ToString(), obj, Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString())));
					//Debug.Log(String.Format("{0} {1}", reader[0], reader[1]));
                    
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

    public static List<Furniture> GetExterior() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='exterior')");
	public static List<Furniture> GetInterior() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='interior')");
	public static List<Furniture> GetDecore() => GetObjects("SELECT * FROM furniture WHERE type=(SELECT id FROM types WHERE type='decore')");

    public static void SaveFurniture(int saveId, string pref, float x, float y)
    {
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			//Debug.Log("DB open for save");
			try
			{
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				//Debug.Log($"INSERT INTO objects_save VALUES ((SELECT id FROM furniture WHERE prefab='{pref}'),{x},{y})");
				cmd.CommandText = $"REPLACE INTO objects_save (id, object_id, x_pos, y_pos) VALUES ({saveId},(SELECT id FROM furniture WHERE prefab='{pref}'),@x,@y)";
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
					saveObjects.Add(new SaveObj(Int32.Parse(reader[0].ToString()), Int32.Parse(reader[1].ToString()), float.Parse(reader[2].ToString()), float.Parse(reader[3].ToString())));
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

	public static string GetType(int id)
	{
		DbConnection.Open();
		if (DbConnection.State == ConnectionState.Open)
		{
			try
			{
				string typeTitle = "";
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = DbConnection;
				cmd.CommandText = $"SELECT type FROM types WHERE id={id}";
				SqliteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					typeTitle = reader[0].ToString();
				}
				DbConnection.Close();
				return typeTitle;
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

	//public static void DeleteFromSave(str)
}
