using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSettings", menuName = "ScriptableObjects/AutoChess/Shop/Settings")]
public class ShopSettingsSO : ScriptableObject
{
    [SerializeField] private int m_refreshCost;
    [SerializeField] private int m_levelCost;
    [SerializeField] private int m_unitsAmount;
    [SerializeField] private List<ChessUnitSO> m_units;
    [SerializeField] private List<ChessUnitProbability> m_probabilities;

    public int RefreshCost => m_refreshCost;
    public int LevelCost => m_levelCost;
    public int UnitsAmounts => m_unitsAmount;
    public List<ChessUnitSO> Units => m_units;
    public List<ChessUnitProbability> Probabilities => m_probabilities;
}
