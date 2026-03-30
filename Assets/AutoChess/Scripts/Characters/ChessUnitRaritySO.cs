using UnityEngine;

[CreateAssetMenu(fileName = "ChessUnit_Rarity_", menuName = "ScriptableObjects/AutoChess/Unit/Rarity")]
public class ChessUnitRaritySO : ScriptableObject
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_name;
    [SerializeField] private int m_cost;
    [SerializeField] private Color m_color;

    public string ID => m_id;
    public string Name => m_name;
    public int Cost => m_cost;
    public Color Color => m_color;
}
