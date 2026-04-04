using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BoardView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_unitsParent;
    [SerializeField] private SlotViewManager m_boardSlotManager;
    [SerializeField] private SlotViewManager m_benchSlotManager;

    [Header("Prefabs")]
    [SerializeField] private ChessUnitHolderView m_chessUnitHolderPrefab;

    [Header("Events")]
    public UnityEvent<int, int> OnBoardViewChanged;
    public UnityEvent<int, int> OnBoardLevelUp;

    private GameState m_gameState;
    private BoardManager m_boardManager;

    private void Awake()
    {
        GameManager.Instance.OnGameContextReady.AddListener(Setup);

        m_boardSlotManager.OnItemEnterGroup.AddListener(HandleUnitEnterBoard);
        m_benchSlotManager.OnItemEnterGroup.AddListener(HandleUnitEnterBench);
    }

    private void Setup(GameContext gameContext)
    {
        m_gameState = gameContext.State;
        m_boardManager = gameContext.Services.BoardManager;

        m_boardManager.OnChessUnitAdded += HandleChessUnitAdded;
        m_boardManager.OnChessUnitRemoved += HandleChessUnitRemoved;
        m_boardManager.OnBoardLevelUp += HandleBoardLevelUP;

        TriggerBoardChangeEvent();
    }

    private void HandleUnitEnterBoard(ChessUnitHolderView unitHolder)
    {
        m_boardManager.MoveUnitToBoard(unitHolder.Unit);

        TriggerBoardChangeEvent();
    }

    private void HandleUnitEnterBench(ChessUnitHolderView unitHolder)
    {
        m_boardManager.MoveUnitToBench(unitHolder.Unit);

        TriggerBoardChangeEvent();
    }

    private void HandleBoardLevelUP()
    {
        OnBoardLevelUp?.Invoke(m_gameState.BoardState.BoardUnits.Count, m_gameState.BoardState.MaxBoardSize);
    }

    private void HandleChessUnitRemoved(ChessUnitRuntime unit, bool inBench)
    {
        Debug.Log($"REMOVE UNIT EVENT {unit.ChessUnitSO.Name}");


        var slotManager = inBench ? m_benchSlotManager : m_boardSlotManager;
        var slot = slotManager.Slots.Where(s => s.CurrentItem != null).ToList().Find(s => s.CurrentItem.Unit == unit);

        if (slot == null)
        {
            Debug.LogError($"[{GetType()}][HandleChessUnitRemoved] Unit '{unit.ChessUnitSO.Name}' nao encontrado nos slots (bench={inBench})");
            return;
        }

        Destroy(slot.CurrentItem.gameObject);
        slot.RmvItem();

        TriggerBoardChangeEvent();
    }

    private void HandleChessUnitAdded(ChessUnitRuntime unit, bool inBench)
    {
        var unitHolder = Instantiate(m_chessUnitHolderPrefab, m_unitsParent);
        unitHolder.Setup(unit);

        var slotManager = inBench ? m_benchSlotManager : m_boardSlotManager;

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

        TriggerBoardChangeEvent();
    }

    private void TriggerBoardChangeEvent() => OnBoardViewChanged?.Invoke(m_gameState.BoardState.BoardUnits.Count, m_gameState.BoardState.MaxBoardSize);
}
