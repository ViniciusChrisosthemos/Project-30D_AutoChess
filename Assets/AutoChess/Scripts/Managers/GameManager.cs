using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    [SerializeField] private ShopSettingsSO m_shopSettingsSO;

    public UnityEvent<GameContext> OnGameContextReady;

    private void Start()
    {
        var gameState = new GameState(50, 1, 1, new List<ChessUnitRuntime>(), 8, new List<ChessUnitRuntime>());

        var boardManager = new BoardManager(gameState);

        var shopSettingsRuntime = new ShopSettingsRuntime(m_shopSettingsSO.RefreshCost, m_shopSettingsSO.LevelCost, m_shopSettingsSO.UnitsAmounts, m_shopSettingsSO.Units, m_shopSettingsSO.Probabilities);
        var shopManager = new ShopManager(shopSettingsRuntime, boardManager, gameState);


        var gameServices = new GameServices(shopManager, boardManager);

        GameContext = new GameContext(gameState, gameServices);

        OnGameContextReady?.Invoke(GameContext);
    }

    public GameContext GameContext { get; private set; }
}
