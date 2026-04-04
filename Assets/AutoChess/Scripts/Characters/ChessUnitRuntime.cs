using System;
using UnityEngine;

public class ChessUnitRuntime
{
    public event Action OnUnitDead;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnManaChanged;
    public event Action OnUnitLevelUp;

    private int m_currentHealth;
    private int m_currentMana;

    public ChessUnitRuntime(ChessUnitSO chessUnitSO, int level, int cost)
    {
        ChessUnitSO = chessUnitSO;
        Level = level;
        Cost = cost;

        ResetStatus();
    }

    public void ResetStatus()
    {
        CurrentHealth = 10;
        CurrentMana = 5;
    }

    public void DecreaseHealth(int value)
    {
        CurrentHealth -= value;

        if (CurrentHealth <= 0)
        {
            OnUnitDead?.Invoke();
        }
    }

    public void AddMana(int value)
    {
        CurrentMana += value;
    }

    public void RmvMana(int value)
    {
        CurrentMana -= value;
    }

    public void Upgrade()
    {
        Level += 1;

        OnUnitLevelUp?.Invoke();
    }

    public ChessUnitSO ChessUnitSO { get; private set; }
    public int Level { get; private set; }
    public int Cost { get; private set; }

    public int CurrentHealth { get { return m_currentHealth; } private set { m_currentHealth = value; OnHealthChanged?.Invoke(m_currentHealth); } }
    public int CurrentMana { get { return m_currentMana; } private set { m_currentMana = value; OnManaChanged?.Invoke(m_currentMana); } }
}
