using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FurnitureTypesList;

public class TaskSystem : MonoBehaviour
{
    private static TaskSO _task;
    public static Action<TaskSO> onStartedTask;

    public static TaskSO CurrentTask { get { return _task; } private set { _task = value; } }

    private void Start()
    {
        TaskSO newTask = new TaskSO("Creative", RoomType.None, Theme.None, FurnitureColor.None);
        onStartedTask.Invoke(newTask);
    }

    private void OnEnable()
    {
        onStartedTask += Test;
    }

    private void Test(TaskSO taskSO)
    {
        Debug.Log("Task start");
    }
}
