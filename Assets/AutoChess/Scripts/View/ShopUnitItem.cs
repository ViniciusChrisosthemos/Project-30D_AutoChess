using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopUnitItem : UIItemController
{
    [SerializeField] private GameObject m_unitView;
    [SerializeField] private Image m_unitIcon;
    [SerializeField] private Image m_background;
    [SerializeField] private TextMeshProUGUI m_unitName;
    [SerializeField] private TextMeshProUGUI m_unitCost;

    private Button m_btnButton;

    protected override void HandleInit(object obj)
    {
        Unit = obj as ChessUnitRuntime;

        m_unitView.SetActive(true);
        m_unitIcon.sprite = Unit.ChessUnitSO.Icon;
        m_unitCost.text = Unit.ChessUnitSO.Name;

        m_btnButton = GetComponent<Button>();
    }

    public void SetInteractable(bool isInteractable)
    {
        m_btnButton.interactable = isInteractable;
    }

    public void SetAvailable(bool isAvailable)
    {
        m_unitView.SetActive(isAvailable);
    }

    public ChessUnitRuntime Unit { get; private set; }
}
