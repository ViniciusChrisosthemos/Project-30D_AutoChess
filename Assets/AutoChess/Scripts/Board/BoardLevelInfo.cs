using System;
using UnityEngine;

[Serializable]
public class BoardLevelInfo
{
    [SerializeField] private int m_cost;
    [SerializeField] private int m_newBoardSize;

    public int Cost => m_cost;
    public int NewBoardSize => m_newBoardSize;
}
