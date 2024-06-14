using System;
using System.Collections;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    public event EventHandler<ScoreChangeEventArgs> OnScoreChangeEvent; 
    public class ScoreChangeEventArgs  : EventArgs
    {
       public int score;
    }
    [SerializeField] private Grid grid;
    private int maxCellValue;
    private bool animationEnded = false;
    public enum MovementDirection
    {
        Right,
        Left,
        RightUp,
        RightDown,
        LeftUp,
        LeftDown
    }
    
    public void MoveRight()
    {
        foreach (Raw raw in grid.GetRawsInGrid())
        {
            for (int cell = raw.rawCells.Length -2 ; cell >=0 ; cell--)
            {
                Cell thisCell = raw.rawCells[cell];
                MoveThisCellInDirection(thisCell , MovementDirection.Right);
            }
        }
    }
    public void MoveLeft()
    {
        foreach (Raw raw in grid.GetRawsInGrid())
        {
            for (int cell = 0 ; cell < raw.rawCells.Length ; cell++)
            {
                Cell thisCell = raw.rawCells[cell];
                MoveThisCellInDirection(thisCell , MovementDirection.Left);
            }
        }
    }
    public void MoveLeftUpOrRightUp(MovementDirection direction)
    {
        for(int raw = 1 ; raw < grid.GetRawsInGrid().Length ; raw ++)
        {
            foreach (Cell cell in grid.GetRawsInGrid()[raw].rawCells)
            {
                MoveThisCellInDirection(cell ,direction);
            }
        }
    }
    public void MoveLeftDownOrRightDown(MovementDirection direction)
    {
        Raw[] RawsArray = grid.GetRawsInGrid();
        for (int raw = RawsArray.Length - 2; raw >= 0; raw--)
        {
            Cell[] currentRawCells = RawsArray[raw].rawCells;
            foreach (Cell cell in currentRawCells ) 
                MoveThisCellInDirection(cell, direction);
        }
    }
    private void MoveThisCellInDirection(Cell thisCell, MovementDirection movementDirection )
    {
        if(thisCell.empty)
            return;

        Cell nextCell = GetNextCellWithDirection(thisCell, movementDirection);
        while ( nextCell != null && nextCell.empty)
        {
            animationEnded = false;
            thisCell.tile.SetParentCell(nextCell);
            nextCell.tile = thisCell.tile;
            thisCell.Clear();
            thisCell = nextCell;
            nextCell = GetNextCellWithDirection(thisCell, movementDirection);
        }
        
        if( nextCell != null && nextCell.tile.GetTileValue() == thisCell.tile.GetTileValue() )
        {
            int newCellNumber = nextCell.tile.GetTileValue() + thisCell.tile.GetTileValue() ;
            InvokeOnScoreChangeEvent(newCellNumber);
            maxCellValue = Mathf.Max(maxCellValue, newCellNumber);
            nextCell.tile.SetTileColorAndValue(newCellNumber);
            Destroy(thisCell.tile.gameObject);
            thisCell.Clear();
          
        }
    }
    public Cell GetNextCellWithDirection(Cell thisCell,  MovementDirection movementDirection)
    {
        if(movementDirection == MovementDirection.RightDown) 
            return GetNextRightDownCell(thisCell);
        if(movementDirection == MovementDirection.RightUp) 
            return GetNextRightUpCell(thisCell);
        if(movementDirection == MovementDirection.LeftUp) 
            return GetNextLeftUp(thisCell);
        if( movementDirection == MovementDirection.LeftDown) 
            return GetNextLeftDown(thisCell);
        if (movementDirection == MovementDirection.Right)
            return GetNextRightCell(thisCell);
        if (movementDirection == MovementDirection.Left) 
            return GetNextLeftCell(thisCell);
        return null;
    }
    private Cell GetNextRightCell(Cell thisCell)
    {
        Vector2 nextCellCoordinates = Vector2.zero;
        nextCellCoordinates = new Vector2(thisCell.coordinates.x + 1, thisCell.coordinates.y);
        return  grid.GetCellInSpecificDirection(nextCellCoordinates);
    }
    private Cell GetNextLeftCell(Cell thisCell)
    {
        Vector2 nextCellCoordinates = Vector2.zero;
        nextCellCoordinates = new Vector2(thisCell.coordinates.x - 1, thisCell.coordinates.y);
        return  grid.GetCellInSpecificDirection(nextCellCoordinates);
    }
    private Cell GetNextRightDownCell(Cell currentCell)
    {

        if ((int)currentCell.coordinates.y+1 >= grid.GetRawsInGrid().Length )
            return null;
        int nextRawLength = grid.GetRawsInGrid()[(int)currentCell.coordinates.y + 1].rawCells.Length;
        int currentRawCellsLenght = grid.GetRawsInGrid()[(int)currentCell.coordinates.y ].rawCells.Length;
        Vector2 nextCellCoordinates ;
        if(nextRawLength > currentRawCellsLenght) 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x+1 , currentCell.coordinates.y+1);
        else 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x , currentCell.coordinates.y+1);

        return grid.GetCellInSpecificDirection(nextCellCoordinates);
    }
    private Cell GetNextRightUpCell(Cell currentCell)
    {

        if ((int)currentCell.coordinates.y-1 <0)
            return null;
        int nextRawLength = grid.GetRawsInGrid()[(int)currentCell.coordinates.y - 1].rawCells.Length;
        int currentRawCellsLenght = grid.GetRawsInGrid()[(int)currentCell.coordinates.y ].rawCells.Length;
        Vector2 nextCellCoordinates ;
        if(nextRawLength > currentRawCellsLenght) 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x+1 , currentCell.coordinates.y-1);
        else 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x , currentCell.coordinates.y-1);

        return grid.GetCellInSpecificDirection(nextCellCoordinates);
    }
    private Cell GetNextLeftUp(Cell currentCell)
    {

        if ((int)currentCell.coordinates.y - 1 < 0)
            return null;
        int nextRawLength = grid.GetRawsInGrid()[(int)currentCell.coordinates.y - 1].rawCells.Length;
        int currentRawCellsLenght = grid.GetRawsInGrid()[(int)currentCell.coordinates.y ].rawCells.Length;
        Vector2 nextCellCoordinates ;
        if(nextRawLength < currentRawCellsLenght) 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x - 1, currentCell.coordinates.y - 1);
   
        else 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x, currentCell.coordinates.y - 1);

        return grid.GetCellInSpecificDirection(nextCellCoordinates);
    }
    private Cell GetNextLeftDown(Cell currentCell)
    {

        if ((int)currentCell.coordinates.y+1 >= grid.GetRawsInGrid().Length )
            return null;
        int nextRawLength = grid.GetRawsInGrid()[(int)currentCell.coordinates.y + 1].rawCells.Length;
        int currentRawCellsLenght = grid.GetRawsInGrid()[(int)currentCell.coordinates.y ].rawCells.Length;
        Vector2 nextCellCoordinates ;
        if(nextRawLength < currentRawCellsLenght) 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x - 1, currentCell.coordinates.y + 1);
        else 
            nextCellCoordinates = new Vector2(currentCell.coordinates.x, currentCell.coordinates.y + 1);
        return grid.GetCellInSpecificDirection(nextCellCoordinates);
    }

    public int CheckIfThereIsMovementAvilable()
    {
        int numberOfMovementsAvilable = 0;
        foreach (Raw raw in grid.GetRawsInGrid())
        {
            foreach (Cell cell in raw.rawCells)
            {
                if(cell.IsCenterCell)
                    continue;
                if (cell.empty)
                {
                    numberOfMovementsAvilable++;
                    continue;
                }
                Cell nextCell = GetNextCellWithDirection(cell , MovementDirection.Right);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
                nextCell = GetNextCellWithDirection(cell , MovementDirection.Left);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
                nextCell = GetNextCellWithDirection(cell , MovementDirection.RightDown);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
                nextCell = GetNextCellWithDirection(cell , MovementDirection.RightUp);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
                nextCell = GetNextCellWithDirection(cell , MovementDirection.LeftUp);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
                nextCell = GetNextCellWithDirection(cell , MovementDirection.LeftDown);
                if (CanSumTheseCells(cell,  nextCell))
                    numberOfMovementsAvilable++;
            }
        }
        return numberOfMovementsAvilable;
    }

    public int GetMaxValueOnCells()
    {
        return maxCellValue;
    }
    private bool CanSumTheseCells(Cell currentCell, Cell nextCell)
    {
        if (nextCell != null && (nextCell.empty || nextCell.tile.GetTileValue() == currentCell.tile.GetTileValue()))
            return true;
        return false;
    }

    private void InvokeOnScoreChangeEvent(int newScore)
    {
        OnScoreChangeEvent?.Invoke(this, new ScoreChangeEventArgs
        {
            score = newScore
        });
    }
}
