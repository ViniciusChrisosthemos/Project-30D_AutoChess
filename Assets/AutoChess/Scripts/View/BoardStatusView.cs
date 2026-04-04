using TMPro;
using UnityEngine;

public class BoardStatusView : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private BoardView m_boardView;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_txtBoardStatusView;

    [Header("Parameters")]
    [SerializeField] private Color m_colorNormalBoardCount = Color.white;
    [SerializeField] private Color m_colorWrongeBoardCount = Color.red;

    private void Awake()
    {
        m_boardView.OnBoardViewChanged.AddListener(HandleBoardViewChanged);
        m_boardView.OnBoardLevelUp.AddListener(HandleBoardViewChanged);
    }

    private void HandleBoardViewChanged(int boardCount, int maxBoardSize)
    {
        m_txtBoardStatusView.text = $"{boardCount}/{maxBoardSize}";

        m_txtBoardStatusView.color = boardCount > maxBoardSize ? m_colorWrongeBoardCount : m_colorNormalBoardCount;
    }
}
