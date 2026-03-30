using NUnit.Framework;
using System.Collections.Generic;

public class ShopSettingsRuntime
{
    public ShopSettingsRuntime(int refreshCost, int levelCost, int unitAmounts, List<ChessUnitSO> units, List<ChessUnitProbability> probabilities)
    {
        RefreshCost = refreshCost;
        LevelCost = levelCost;
        UnitAmounts = unitAmounts; ;
        Units = units;
        Probabilities = probabilities;
    }

    public int RefreshCost { get; private set; }
    public int LevelCost { get; private set; }
    public int UnitAmounts { get; private set; }
    public List<ChessUnitSO> Units { get; private set; }
    public List<ChessUnitProbability> Probabilities { get; private set; }
}
