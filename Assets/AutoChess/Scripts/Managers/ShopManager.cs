using System.Collections.Generic;
using System.Linq;

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
        var unitSO = m_pollGenerator.GetRandomPoll(m_shopSettingsRuntime.UnitAmounts);

        AvailableUnits = unitSO.Select(unit => new ChessUnitRuntime(unit, 1, unit.Rarity.Cost)).ToList();
    }

    public int GetRefreshCost() => m_shopSettingsRuntime.RefreshCost;

    public int GetLevelCost() => m_shopSettingsRuntime.LevelCost;

    public List<ChessUnitRuntime> AvailableUnits { get; private set; }
}
