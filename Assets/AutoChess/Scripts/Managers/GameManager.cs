using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    [SerializeField] private BoardLevelProgressionSO m_boardLevelProgressionSO;
    [SerializeField] private ShopSettingsSO m_shopSettingsSO;

    public UnityEvent<GameContext> OnGameContextReady;

    private void Start()
    {
        var boardLevelInfo = m_boardLevelProgressionSO.BoardLevelsInfo;
        var maxBenchSize = 8;
        var boardState = new BoardState(0, boardLevelInfo, new(), maxBenchSize, new());

        var gameState = new GameState(10, boardState);

        var boardManager = new BoardManager(gameState);

        var shopSettingsRuntime = new ShopSettingsRuntime(m_shopSettingsSO.RefreshCost, m_shopSettingsSO.LevelCost, m_shopSettingsSO.UnitsAmounts, m_shopSettingsSO.Units, m_shopSettingsSO.Probabilities);
        var shopManager = new ShopManager(shopSettingsRuntime, boardManager, gameState);


        var gameServices = new GameServices(shopManager, boardManager);

        GameContext = new GameContext(gameState, gameServices);

        OnGameContextReady?.Invoke(GameContext);
    }

    public GameContext GameContext { get; private set; }
}
