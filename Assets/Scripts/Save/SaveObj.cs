using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObj
{
	public int Id { get; private set; }
	public int Object_Id { get; private set; }
	public float X { get; private set; }
    public float Y { get; private set; }

	public SaveObj(int id, int object_id, float x, float y)
	{
		Id = id;
		Object_Id = object_id;
		X = x;
		Y = y;
	}
}
