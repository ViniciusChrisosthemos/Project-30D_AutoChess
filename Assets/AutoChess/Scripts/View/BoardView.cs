using System.Linq;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private Transform m_unitsParent;
    [SerializeField] private SlotViewManager m_boardSlotManager;

    [Header("Prefabs")]
    [SerializeField] private ChessUnitHolderView m_chessUnitHolderPrefab;

    private GameState m_gameState;
    private BoardManager m_boardManager;

    private void Awake()
    {
        GameManager.Instance.OnGameContextReady.AddListener(Setup);


    }

    private void Setup(GameContext gameContext)
    {
        m_gameState = gameContext.State;
        m_boardManager = gameContext.Services.BoardManager;

        m_boardManager.OnChessUnitAdded += HandleChessUnitAdded;
        m_boardManager.OnChessUnitRemoved += HandleChessUnitRemoved;
    }

    private void HandleChessUnitRemoved(ChessUnitRuntime unit, bool inBench)
    {

    }

    private void HandleChessUnitAdded(ChessUnitRuntime unit, bool inBench)
    {
        var unitHolder = Instantiate(m_chessUnitHolderPrefab, m_unitsParent);
        unitHolder.Setup(unit);

        //var slotManager = inBench ? m_benchSlotManager : m_boardSlotManager;

        var slotManager = m_boardSlotManager;

        foreach (var slot in slotManager.Slots)
        {
            if (slot.CurrentItem == null)
            {
                unitHolder.transform.position = slot.transform.position;
                unitHolder.transform.rotation = slot.transform.rotation;

                slot.SetItem(unitHolder);
                break;
            }
        }
    }
}
