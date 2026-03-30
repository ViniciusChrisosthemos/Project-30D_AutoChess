using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager
{
    private PlayerManager m_playerManager;
    private ShopSettingsRuntime m_shopSettingsRuntime;
    private ChessUnitPollGenerator m_pollGenerator;

    public ShopManager(ShopSettingsRuntime shopSettings, PlayerManager playerManager)
    {
        m_playerManager = playerManager;
        m_shopSettingsRuntime = shopSettings;

        m_pollGenerator = new ChessUnitPollGenerator(shopSettings.Units, shopSettings.Probabilities);
        UpdatePoll();
    }

    public bool CanBuyRefresh()
    {
        return m_playerManager.Gold >= m_shopSettingsRuntime.RefreshCost;
    }

    public bool CanBuyLevel()
    {
        return m_playerManager.Gold >= m_shopSettingsRuntime.LevelCost;
    }

    public bool CanBuyCharacter(ChessUnitRuntime unit)
    {
        return m_playerManager.Gold >= unit.Cost;
    }

    public void BuyCharacter(ChessUnitRuntime unit)
    {
        AvailableUnits.Remove(unit);
        m_playerManager.RmvGold(unit.Cost);
    }

    public void BuyRefresh()
    {
        m_playerManager.RmvGold(m_shopSettingsRuntime.RefreshCost);

        UpdatePoll();
    }

    public void BuyLevel()
    {
        m_playerManager.RmvGold(m_shopSettingsRuntime.LevelCost);
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
