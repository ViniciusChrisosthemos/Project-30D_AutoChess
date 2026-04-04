using TMPro;
using UnityEngine;

public class UIPlayerStatusView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_txtGold;

    private GameContext m_gameContext;

    private void Awake()
    {
        GameManager.Instance.OnGameContextReady.AddListener(HandleGameContextReady);
    }

    private void HandleGameContextReady(GameContext context)
    {
        m_gameContext = context;

        context.State.OnGoldChanged += SetGoldText;

        SetGoldText(context.State.Gold);
    }

    private void SetGoldText(int value)
    {
        m_txtGold.text = value.ToString();
    }
}
