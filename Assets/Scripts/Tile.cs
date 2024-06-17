using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public event EventHandler<TileAnimationEventArgs> OnAnimate; 
    public class TileAnimationEventArgs : EventArgs
    {
        public TileAnimation.AnimationState State;
        public Vector3 To;
        public bool Merge;
    }
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

    public void InvokeAnimationEvent(TileAnimation.AnimationState state, Vector3 to , bool merge )
    {
        OnAnimate?.Invoke(this , new TileAnimationEventArgs
        {
            State = state,
            To = to,
            Merge = merge,
        });
    }
    public int GetTileValue()
    {
        return int.Parse(tileText.text);
    }
    
    public void SetTileOnCell(Cell cell)
    {
        cell.tile = this;
        cell.empty = false;
    }

    public Color GetBackgroundColor()
    {
        return tileBackGround.color;
    }
    
}
