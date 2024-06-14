using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cell : MonoBehaviour
{
    public bool empty = true;
    public bool IsCenterCell = false;
    public Vector2 coordinates;
    public Tile tile;

    public void Clear()
    {
        empty = true;
        tile = null;
    }
    
}
