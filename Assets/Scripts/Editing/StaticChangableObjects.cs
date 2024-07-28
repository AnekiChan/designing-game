using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FurnitureTypesList;
using System;

public class StaticChangableObjects : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _wall;

    public GameObject Floor => _floor;
    public GameObject Wall => _wall;

    private void OnEnable()
    {
        EventBus.Instance.ChangeFloor += ChangeFloor;
        EventBus.Instance.ChangeWall += ChangeWall;
    }

    private void OnDisable()
    {
        EventBus.Instance.ChangeWall -= ChangeWall;
        EventBus.Instance.ChangeFloor -= ChangeFloor;
    }

    private void ChangeFloor(FurnitureSO floor)
    {
        if (floor.FurnitureType == FurnitureType.Floor)
        {
            _floor.GetComponent<SpriteRenderer>().sprite = floor.Prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void ChangeWall(FurnitureSO wall)
    {
        if (wall.FurnitureType == FurnitureType.Wall)
        {
            _wall.GetComponent<SpriteRenderer>().sprite = wall.Prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
