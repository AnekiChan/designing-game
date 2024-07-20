using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FurnitureTypesList;

public class TaskSystem : MonoBehaviour
{
    private static TaskSO _task;

    public static TaskSO CurrentTask { get { return _task; } private set { _task = value; } }

    private void Start()
    {
        Debug.Log("s");
        CurrentTask = new TaskSO("Creative", RoomType.None, Theme.None, FurnitureColor.None);
        
    }

    public static List<FurnitureSO> GetFurnitureToTask()
    {
        CurrentTask = new TaskSO("Creative", RoomType.None, Theme.None, FurnitureColor.None);
        if (CurrentTask != null)
        {
            return FurnitureSystem.AllFurniture.FindAll(
                p => p.RoomTypes.Contains(CurrentTask.RoomType) && p.Theme.Contains(CurrentTask.Theme) && p.FurnitureColors.Contains(CurrentTask.Colors));
        }
        else throw new ArgumentNullException("No task");
    }
}
