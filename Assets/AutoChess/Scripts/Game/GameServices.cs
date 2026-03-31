using UnityEngine;

public class GameServices
{
    public ShopManager ShopManager { get; private set; }
    public BoardManager BoardManager { get; private set; }

    public GameServices(ShopManager shopManager, BoardManager boardManager)
    {
        ShopManager = shopManager;
        BoardManager = boardManager;
    }
}
