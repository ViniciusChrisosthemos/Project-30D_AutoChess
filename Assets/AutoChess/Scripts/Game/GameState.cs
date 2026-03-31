using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public GameState(int gold, int level, int maxBoardSize, List<ChessUnitRuntime> boadUnits, int maxBenchUnits, List<ChessUnitRuntime> benchUnits)
    {
        Gold = gold;
        Level = level;
        MaxBoardSize = maxBoardSize;
        BoadUnits = boadUnits;
        MaxBenchUnits = maxBenchUnits;
        BenchUnits = benchUnits;
    }

    public int Gold {  get; set; }
    public int Level { get; set; }
    public int MaxBoardSize { get; set; }
    public List<ChessUnitRuntime> BoadUnits { get; set; }
    public int MaxBenchUnits { get; set; }
    public List<ChessUnitRuntime> BenchUnits { get; set; }
}
