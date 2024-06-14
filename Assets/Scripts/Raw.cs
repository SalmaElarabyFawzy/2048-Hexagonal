using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raw : MonoBehaviour
{
    public Cell[] rawCells;
    private void Awake()
    {
        rawCells = GetComponentsInChildren<Cell>();
    }
    
}
