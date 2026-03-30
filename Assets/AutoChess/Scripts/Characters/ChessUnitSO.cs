using UnityEngine;

[CreateAssetMenu(fileName = "ChessUnit_", menuName = "ScriptableObjects/AutoChess/Unit/Unit")]
public class ChessUnitSO : ScriptableObject
{
    [SerializeField] private string m_id;
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private ChessUnitRaritySO m_rarity;
    [SerializeField] private ChessUnitModelReferences m_modelReference;

    public string ID { get { return m_id; } }
    public string Name { get { return m_name; } }
    public Sprite Icon { get { return m_icon; } }
    public ChessUnitRaritySO Rarity {  get { return m_rarity; } }
    public ChessUnitModelReferences ModelReferences { get { return m_modelReference; } }
}
