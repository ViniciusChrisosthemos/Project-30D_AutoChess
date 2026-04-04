using System.Linq;
using UnityEngine;

public class UnitUpgradeProcessor : IBoardRule
{
    private const int UNIT_AMOUNT_TO_UPGRADE = 3;

    public void ProcessBoard(BoardManager boardManager)
    {
        var allUnits = boardManager.GetAllUnits();

        var groups = allUnits.GroupBy(unit => (unit.ChessUnitSO.ID, unit.Level));

        Debug.Log("Groups");
        foreach (var group in groups)
        {
            Debug.Log($"   {group.ElementAt(0).ChessUnitSO.ID} {group.ElementAt(0).Level} | {group.Count()}");
        }

        var needNewUpdate = false;

        foreach (var group in groups)
        {
            if (group.Count() >= UNIT_AMOUNT_TO_UPGRADE)
            {
                Debug.Log($"[{GetType()}][ProcessBoard] Group elegivel para upgrade encontrado: {group.ElementAt(0).ChessUnitSO.Name} - {group.Count()}");

                var selectedUnits = group.Take(UNIT_AMOUNT_TO_UPGRADE).ToList();

                var unitToUpgrade = selectedUnits[0];
                unitToUpgrade.Upgrade();

                var unitsToDelete = selectedUnits.GetRange(1, UNIT_AMOUNT_TO_UPGRADE - 1);
                unitsToDelete.ForEach(unit => boardManager.RmvUnit(unit));
                
                needNewUpdate = true;
            }
        }

        if (needNewUpdate)
        {
            ProcessBoard(boardManager);
        }
    }
}
