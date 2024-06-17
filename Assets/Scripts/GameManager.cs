using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameOver;
    public event EventHandler OnWinning;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Grid grid;
    [SerializeField] private CellsManager cellsController;
    [SerializeField] private int desiredValue = 2048;
    private int numberOfRespones = 2;
    private int timeUntilFourRespone = 0;
    private bool wait = true;
    private void Awake()
    {
        gameInput.OnKeysPressed += GetInput;
    }

    private void Start()
    {
        ResponeNewTile();
    }

    private void GetInput(object sender, GameInput.KeysPressedEventArgs keys)
    {
        if(wait)
        {
            if (keys.KeysPressed == CellsManager.MovementDirection.Right)
            {
                cellsController.MoveRight();
            }

            if (keys.KeysPressed == CellsManager.MovementDirection.Left)
            {
                cellsController.MoveLeft();
            }

            if (keys.KeysPressed == CellsManager.MovementDirection.LeftUp ||
                keys.KeysPressed == CellsManager.MovementDirection.RightUp)
            {
                cellsController.MoveLeftUpOrRightUp(keys.KeysPressed);
            }

            if (keys.KeysPressed == CellsManager.MovementDirection.RightDown ||
                keys.KeysPressed == CellsManager.MovementDirection.LeftDown)
            {
                cellsController.MoveLeftDownOrRightDown(keys.KeysPressed);
            }

            StartCoroutine(WaitUntilMoveComplete());
            ResponeNewTile();
            if (cellsController.GetMaxValueOnCells() >= desiredValue)
            {
                InvokeWinningEvent();
                Debug.Log("WIN");
            }
            else
            {
                if (!cellsController.IsThereMovementAvilable())
                {
                    InvokeGameOverEvent();
                    Debug.Log("Game Over");
                }
            }
        }
    }
    private void ResponeNewTile()
    {
        List<Cell> emptyCellsList =grid.GetEmptyCellsInGrid();
        if (emptyCellsList.Count == 1)
            numberOfRespones = 1;
        else if(emptyCellsList.Count >=2)
            numberOfRespones = 2;
        else 
            return;
        
        for (int responeNumber = 0; responeNumber < numberOfRespones; responeNumber++)
        {
            int randomIndex = Random.Range(0, emptyCellsList.Count);
            Cell emptyCell = emptyCellsList[randomIndex];
            Tile newTile = Instantiate(tilePrefab , grid.transform);
            newTile.SetTileOnCell(emptyCell);
            newTile.transform.position = emptyCell.transform.position;
            if (timeUntilFourRespone == 10)
            {
                newTile.SetTileColorAndValue(4);
                timeUntilFourRespone = 0;
            }
            else
            {
                newTile.SetTileColorAndValue(2);
                timeUntilFourRespone++;
            }
            newTile.InvokeAnimationEvent(TileAnimation.AnimationState.Fade , Vector3.zero , false);
            emptyCellsList.RemoveAt(randomIndex);
        }
    }
    private void InvokeGameOverEvent()
    {
        OnGameOver?.Invoke(this , EventArgs.Empty);
    }
    private void InvokeWinningEvent()
    {
        OnWinning?.Invoke(this , EventArgs.Empty);
    }

    public void NewGame()
    {
       grid.ClearGrid();
       ResponeNewTile();
    }

    private IEnumerator WaitUntilMoveComplete()
    {
        wait = false;
        yield return new WaitForSeconds(0.1f);
        wait = true;
    }
}
