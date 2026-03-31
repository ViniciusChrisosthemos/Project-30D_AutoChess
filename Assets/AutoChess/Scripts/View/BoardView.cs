using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private SlotViewManager m_boardSlotManager;
    [SerializeField] private SlotViewManager m_benchSlotManager;

    private void Awake()
    {
        GameManager.Instance.OnGameContextReady.AddListener(Setup);

        m_boardSlotManager.OnItemEnterGroup.AddListener(HandleItemEnterBoardView);
        m_benchSlotManager.OnItemEnterGroup.AddListener(HandleItemEnterBenchView);

        m_boardSlotManager.OnItemExitGroup.AddListener(HandleItemExitBoardView);
        m_benchSlotManager.OnItemExitGroup.AddListener(HandleItemExitBenchView);
    }

    private void Setup(GameContext gameContext)
    {
        gameContext.Services.BoardManager.OnChessUnitAdded += HandleChessUnitAdded;
        gameContext.Services.BoardManager.OnChessUnitRemoved += HandleChessUnitRemoved;
    }

    private void HandleItemEnterBoardView(GameObject item)
    {
        Debug.Log($"[{GetType()}] Item '{item.name}' entrou no Board");
    }

    private void HandleItemEnterBenchView(GameObject item)
    {
        Debug.Log($"[{GetType()}] Item '{item.name}' entrou no Bench");
    }

    private void HandleItemExitBoardView(GameObject item)
    {
        Debug.Log($"[{GetType()}] Item '{item.name}' saiu no Board");
    }

    private void HandleItemExitBenchView(GameObject item)
    {
        Debug.Log($"[{GetType()}] Item '{item.name}' saiu no Bench");
    }


    private void HandleChessUnitRemoved(ChessUnitRuntime unit, bool inBench)
    {

    }

    private void HandleChessUnitAdded(ChessUnitRuntime arg1, bool inBench)
    {

    }
}
