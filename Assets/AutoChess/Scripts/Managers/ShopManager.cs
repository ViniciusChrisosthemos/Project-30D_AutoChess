using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager
{
    private GameState m_gameState;
    private ShopSettingsRuntime m_shopSettingsRuntime;
    private ChessUnitPollGenerator m_pollGenerator;

    public ShopManager(ShopSettingsRuntime shopSettings, GameState gameState)
    {
        m_gameState = gameState;
        m_shopSettingsRuntime = shopSettings;

        m_pollGenerator = new ChessUnitPollGenerator(shopSettings.Units, shopSettings.Probabilities);
        UpdatePoll();
    }

    public bool CanBuyRefresh()
    {
        return m_gameState.Gold >= m_shopSettingsRuntime.RefreshCost;
    }

    public bool CanBuyLevel()
    {
        return m_gameState.Gold >= m_shopSettingsRuntime.LevelCost;
    }

    public bool CanBuyCharacter(ChessUnitRuntime unit)
    {
        return m_gameState.Gold >= unit.Cost;
    }

    public void BuyCharacter(ChessUnitRuntime unit)
    {
        AvailableUnits.Remove(unit);
        m_gameState.Gold -= unit.Cost;
    }

    public void BuyRefresh()
    {
        m_gameState.Gold -= m_shopSettingsRuntime.RefreshCost;

        UpdatePoll();
    }

    public void BuyLevel()
    {
        m_gameState.Gold -= m_shopSettingsRuntime.LevelCost;
    }

    private void UpdatePoll()
    {
        var unitsSO = m_pollGenerator.GetRandomPoll(m_shopSettingsRuntime.UnitAmounts);

        AvailableUnits = unitsSO.Select(unit => new ChessUnitRuntime(unit, 1, unit.Rarity.Cost)).ToList();

        Debug.Log($"{m_shopSettingsRuntime.UnitAmounts} {m_shopSettingsRuntime.Units.Count} {unitsSO} {AvailableUnits.Count}");
    }

    public int GetRefreshCost() => m_shopSettingsRuntime.RefreshCost;

    public int GetLevelCost() => m_shopSettingsRuntime.LevelCost;

    public List<ChessUnitRuntime> AvailableUnits { get; private set; }
}
