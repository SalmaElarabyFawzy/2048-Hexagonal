using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private List<TileState> tileStateList;
    [SerializeField] private Image tileBackGround;
    [SerializeField] private TextMeshProUGUI tileText;


    public void SetTileColorAndValue(int tileNumber)
    {
        tileText.text = tileNumber.ToString();
        foreach (var tileState in tileStateList)
        {
            if (tileState.TileNumber == tileNumber)
            {
                tileBackGround.color = tileState.BackGround;
                tileText.color = tileState.TextColor;
            }
        }
    }
    public int GetTileValue()
    {
        return int.Parse(tileText.text);
    }
    
    public void SetParentCell(Cell cell)
    {
     
        transform.position = cell.transform.position;
        cell.tile = this;
        cell.empty = false;
    }
 
}
