using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private  Raw[] gridRaws;
    private List<Cell> emptyCellsList;
     
    private void Awake()
    {
        gridRaws = GetComponentsInChildren<Raw>();
    }

    private void Start()
    {
        for (int y = 0; y < gridRaws.Length; y++)
        {
            for (int x = 0; x < gridRaws[y].rawCells.Length; x++)
            {
                if(gridRaws[y].rawCells[x].IsCenterCell)
                    continue;
                gridRaws[y].rawCells[x].coordinates = new Vector2(x, y);
            }
        }
    }
    public Raw[] GetRawsInGrid()
    {
        return gridRaws;
    }
    public List<Cell> GetEmptyCellsInGrid()
    {
        emptyCellsList = new List<Cell>();
        for (int raw = 0; raw < gridRaws.Length; raw++)
        {
            for (int cell = 0; cell < gridRaws[raw].rawCells.Length; cell++)
            {
                if(gridRaws[raw].rawCells[cell].IsCenterCell)
                    continue;
                if(gridRaws[raw].rawCells[cell].empty) 
                    emptyCellsList.Add(gridRaws[raw].rawCells[cell]);
            }
        }
        return emptyCellsList;
    }
    
    public Cell GetCellInSpecificDirection(Vector2 coordinates)
    {  
        if ((coordinates.y >= 0 && coordinates.y < gridRaws.Length) && (coordinates.x >=0 && coordinates.x < gridRaws[(int)coordinates.y].rawCells.Length))
        {
            Cell requiredCell = gridRaws[(int)coordinates.y].rawCells[(int)coordinates.x];
            if (!requiredCell.IsCenterCell)
                return requiredCell;
        }
        return null;
    }
    public void ClearGrid()
    {
        foreach (Raw raw in gridRaws)
        {
            foreach (Cell cell in raw.rawCells)
            {
                if (!cell.empty)
                {
                    Destroy(cell.tile.gameObject);
                    cell.Clear();
                }
            }
        }
    }
    
}
