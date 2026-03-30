using UnityEngine;

public class PlayerManager
{
    public PlayerManager(int gold)
    {
        Gold = gold;
    }

    public void RmvGold(int gold)
    {
        Gold -= gold;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
    }

    public int Gold {  get; private set; }
}
