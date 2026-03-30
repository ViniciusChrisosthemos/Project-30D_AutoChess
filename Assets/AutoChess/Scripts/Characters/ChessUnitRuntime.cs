using UnityEngine;

public class ChessUnitRuntime
{
    public ChessUnitRuntime(ChessUnitSO chessUnitSO, int level, int cost)
    {
        ChessUnitSO = chessUnitSO;
        Level = level;
        Cost = cost;
    }

    public ChessUnitSO ChessUnitSO { get; private set; }
    public int Level { get; private set; }
    public int Cost { get; private set; }
}
