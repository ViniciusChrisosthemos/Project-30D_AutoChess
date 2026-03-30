using System;
using UnityEngine;

[Serializable]
public class ChessUnitProbability
{
    [SerializeField] private ChessUnitRaritySO m_raritySO;
    [SerializeField] private float m_probability;

    public ChessUnitRaritySO RaritySO => m_raritySO;
    public float Probability => m_probability;
}
