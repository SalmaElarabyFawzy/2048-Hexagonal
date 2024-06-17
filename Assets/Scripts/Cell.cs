using UnityEngine;


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
