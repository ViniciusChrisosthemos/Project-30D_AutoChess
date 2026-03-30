using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessUnitPollGenerator
{
    private Dictionary<ChessUnitRaritySO, List<ChessUnitSO>> m_unitsDict;
    private List<ChessUnitProbability> m_probabilities;

    public ChessUnitPollGenerator(List<ChessUnitSO> units, List<ChessUnitProbability> probabilities)
    {
        m_probabilities = probabilities;

        m_unitsDict = new Dictionary<ChessUnitRaritySO, List<ChessUnitSO>>();

        foreach (var rarity in probabilities)
        {
            var unitsByRarity = units.Where(unit => unit.Rarity == rarity.RaritySO).ToList();

            m_unitsDict.Add(rarity.RaritySO, unitsByRarity);
        }
    }

    public List<ChessUnitSO> GetRandomPoll(int amount)
    {
        var result = new List<ChessUnitSO>();

        for (int i = 0; i < amount; i++)
        {
            var randomValue = Random.Range(0f, 1f);
            var accumProbability = 0f;

            foreach (var probabilityData in m_probabilities)
            {
                accumProbability += probabilityData.Probability;

                if (accumProbability <= randomValue)
                {
                    var randomItem = m_unitsDict[probabilityData.RaritySO].GetRandomItem();

                    result.Add(randomItem);
                    break;
                }
            }
        }

        return result;
    }
}
