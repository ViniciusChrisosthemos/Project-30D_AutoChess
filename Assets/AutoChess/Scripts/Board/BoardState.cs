using System.Collections.Generic;
using UnityEngine;

public class BoardState
{
    public BoardState(int level, List<BoardLevelInfo> boardLevelsInfo, List<ChessUnitRuntime> boardUnits, int maxBenchUnits, List<ChessUnitRuntime> benchUnits)
    {
        Level = level;
        BoardLevelInfos = boardLevelsInfo;
        BoardUnits = boardUnits;
        MaxBenchUnits = maxBenchUnits;
        BenchUnits = benchUnits;
    }

    public int MaxBoardSize => BoardLevelInfos[Level].NewBoardSize;
    public List<ChessUnitRuntime> BoardUnits { get; set; }
    public int MaxBenchUnits { get; set; }
    public List<ChessUnitRuntime> BenchUnits { get; set; }
    public List<BoardLevelInfo> BoardLevelInfos { get; set; }
    public int Level { get; set; }
    public bool IsMaxLevel => Level == BoardLevelInfos.Count - 1;
    public int Experience { get; set; }
    public int ExperienceForNextLevel => Level < BoardLevelInfos.Count - 1 ? BoardLevelInfos[Level].Cost : 0;
}
