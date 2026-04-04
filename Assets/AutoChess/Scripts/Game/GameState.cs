using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public event Action<int> OnGoldChanged;
    
    private int m_gold;

    public GameState(int gold, BoardState boardState)
    {
        m_gold = gold;
        BoardState = boardState;
    }

    public int Gold 
    {
        get { return m_gold; }
        set 
        {
            m_gold = value;
            OnGoldChanged?.Invoke(m_gold);
        }
    }

    public BoardState BoardState { get; set; }
}
