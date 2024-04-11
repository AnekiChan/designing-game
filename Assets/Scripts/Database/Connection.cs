using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Connection : MonoBehaviour
{
    public SqliteConnection DbConnection;
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        SetConnection();
    }

    public void SetConnection()
    {
        path = Application.dataPath + "/AssetsDatabase/db.bytes";
        DbConnection = new SqliteConnection("URI=file:" + path);
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
    }
}
