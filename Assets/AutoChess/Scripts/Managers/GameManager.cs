using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    [SerializeField] private ShopSettingsSO m_shopSettingsSO;

    private PlayerManager m_playerManager;
    private ShopManager m_shopManager;

    private void Awake()
    {
        m_playerManager = new PlayerManager(50);

        var shopSettingsRuntime = new ShopSettingsRuntime(m_shopSettingsSO.RefreshCost, m_shopSettingsSO.LevelCost, m_shopSettingsSO.UnitsAmounts, m_shopSettingsSO.Units, m_shopSettingsSO.Probabilities);
        m_shopManager = new ShopManager(shopSettingsRuntime, m_playerManager);
    }

    public PlayerManager PlayerManager => m_playerManager;
    public ShopManager ShopManager => m_shopManager;
}
