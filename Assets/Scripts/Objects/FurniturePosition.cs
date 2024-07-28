using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePosition
{
    private int _xPos;
    private int _yPos;
    private int _rotation;

    public FurniturePosition(int x, int y, int rotation)
    {
        _xPos = x;
        _yPos = y;
        _rotation = rotation;
    }
}
