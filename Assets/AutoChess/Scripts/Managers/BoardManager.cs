using System;
using UnityEngine;

public class BoardManager
{
    private GameState m_gameState;

    public event Action<ChessUnitRuntime, bool> OnChessUnitAdded;
    public event Action<ChessUnitRuntime, bool> OnChessUnitRemoved;

    public BoardManager(GameState gameState)
    {
        m_gameState = gameState;
    }

    public bool CanAddUnit()
    {
        return !IsBenchFull() || !IsBoardFull();
    }

    public void AddUnit(ChessUnitRuntime unit)
    {
        if (!CanAddUnit()) return;

        bool goToBench;

        if (IsBenchFull())
        {
            m_gameState.BenchUnits.Add(unit);

            goToBench = false;
        }
        else
        {
            m_gameState.BoadUnits.Add(unit);
            
            goToBench = true;
        }

        OnChessUnitAdded?.Invoke(unit, goToBench);
    }

    public void RmvUnit(ChessUnitRuntime unit)
    {
        if (m_gameState.BenchUnits.Contains(unit))
        {
            m_gameState.BenchUnits.Remove(unit);

            OnChessUnitRemoved?.Invoke(unit, true);
        }
        else if (m_gameState.BoadUnits.Contains(unit))
        {
            m_gameState.BoadUnits.Remove(unit);

            OnChessUnitRemoved?.Invoke(unit, false);
        }
    }

    public bool IsBenchFull() => m_gameState.BenchUnits.Count >= m_gameState.MaxBenchUnits;

    public bool IsBoardFull() => m_gameState.BoadUnits.Count >= m_gameState.MaxBoardSize;
}
