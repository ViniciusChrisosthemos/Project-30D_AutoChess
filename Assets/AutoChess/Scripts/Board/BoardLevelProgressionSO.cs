using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardProgression", menuName = "ScriptableObjects/AutoChess/Board/Progression")]
public class BoardLevelProgressionSO : ScriptableObject
{
    [SerializeField] private List<BoardLevelInfo> m_boardLevelsInfo;

    public List<BoardLevelInfo> BoardLevelsInfo => m_boardLevelsInfo;
}
