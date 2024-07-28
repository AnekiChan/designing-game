using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistanceManeger : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistanceManeger Instance { get; private set; }
    private List<IDataPersistance> dataPersistanceObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than on data persistance manager in the scene");
        }
        Instance = this;
    }

    private void Start()
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistancesObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistancesObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("No data was found");
            NewGame();
        }

        foreach(IDataPersistance dataPersistanceObj in  dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
        Debug.Log("Loaded room \n" + gameData.RoomDataList);
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }
        Debug.Log("Saved room");
        foreach (RoomData roomData in gameData.RoomDataList)
        {
            Debug.Log(roomData.Id);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
