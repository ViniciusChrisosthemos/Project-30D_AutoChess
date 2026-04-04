using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    private BoardState m_boardState;

    public event Action<ChessUnitRuntime, bool> OnChessUnitAdded;
    public event Action<ChessUnitRuntime, bool> OnChessUnitRemoved;
    public event Action OnBoardLevelUp;

    private List<IBoardRule> m_boardRules;

    public BoardManager(GameState gameState)
    {
        m_boardState = gameState.BoardState;

        m_boardRules = new List<IBoardRule>();

        m_boardRules.Add(new UnitUpgradeProcessor());
    }

    public bool CanAddUnit()
    {
        return !(IsBenchFull() && IsBoardFull());
    }

    public void AddUnit(ChessUnitRuntime unit)
    {
        if (!CanAddUnit()) return;


        bool goToBench;

        if (IsBenchFull())
        {
            m_boardState.BoardUnits.Add(unit);

            goToBench = false;
        }
        else
        {
            m_boardState.BenchUnits.Add(unit);

            goToBench = true;
        }

        Debug.Log($"{m_boardState.BoardUnits.Count}/{m_boardState.MaxBoardSize}   -   {m_boardState.BenchUnits.Count}/{m_boardState.MaxBenchUnits}");

        OnChessUnitAdded?.Invoke(unit, goToBench);

        ApplyBoardRules();
    }

    public void RmvUnit(ChessUnitRuntime unit)
    {
        if (m_boardState.BenchUnits.Contains(unit))
        {
            m_boardState.BenchUnits.Remove(unit);

            OnChessUnitRemoved?.Invoke(unit, true);
        }
        else if (m_boardState.BoardUnits.Contains(unit))
        {
            m_boardState.BoardUnits.Remove(unit);

            OnChessUnitRemoved?.Invoke(unit, false);
        }
    }

    public void MoveUnitToBoard(ChessUnitRuntime unit)
    {
        m_boardState.BenchUnits.Remove(unit);
        m_boardState.BoardUnits.Add(unit);
    }

    public void MoveUnitToBench(ChessUnitRuntime unit)
    {
        m_boardState.BoardUnits.Remove(unit);
        m_boardState.BenchUnits.Add(unit);
    }

    private void ApplyBoardRules()
    {
        foreach (var rule in m_boardRules)
        {
            rule.ProcessBoard(this);
        }
    }

    public bool IsBenchFull() => m_boardState.BenchUnits.Count >= m_boardState.MaxBenchUnits;

    public bool IsBoardFull() => m_boardState.BoardUnits.Count >= m_boardState.MaxBoardSize;

    public void ChangeBoard(List<ChessUnitRuntime> boardUnits, List<ChessUnitRuntime> benchUnits)
    {
        m_boardState.BoardUnits = boardUnits;
        m_boardState.BenchUnits = benchUnits;

        Debug.Log($"Board changed Board {m_boardState.BoardUnits.Count}/{m_boardState.MaxBoardSize}  |  Bench {m_boardState.BenchUnits.Count}/{m_boardState.MaxBenchUnits}");
    }

    public List<ChessUnitRuntime> GetAllUnits()
    {
        var allUnits = new List<ChessUnitRuntime>(m_boardState.BoardUnits);
        allUnits.AddRange(m_boardState.BenchUnits);

        return allUnits;
    }

    public void Upgrade()
    {
        if (m_boardState.IsMaxLevel) return;

        m_boardState.Level += 1;

        OnBoardLevelUp?.Invoke();
    }
}
