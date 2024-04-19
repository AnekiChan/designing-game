using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public GameObject Prefab;
    public int Type { get; private set; }
    public int Score { get; private set; }

    public Furniture(int id, string title, GameObject prefab, int type, int score)
	{
		Id = id;
		Title = title;
		Prefab = prefab;
		Type = type;
		Score = score;
	}
}
