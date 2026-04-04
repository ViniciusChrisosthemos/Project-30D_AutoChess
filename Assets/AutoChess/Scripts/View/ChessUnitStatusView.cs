using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChessUnitStatusView : MonoBehaviour
{
    [SerializeField] private GameObject m_view;
    [SerializeField] private Slider m_sliderHealth;
    [SerializeField] private Slider m_sliderMana;
    [SerializeField] private TextMeshProUGUI m_txtStarts;

    public void Setup(ChessUnitRuntime unit)
    {
        Unit = unit;

        Unit.OnHealthChanged += arg => UpdateUI();
        Unit.OnManaChanged += arg => UpdateUI();
        Unit.OnUnitDead += () => SetViewVisibility(false);
        Unit.OnUnitLevelUp += () => UpdateUI();

        UpdateUI();
        SetViewVisibility(true);
    }

    private void UpdateUI()
    {
        m_sliderHealth.value = Unit.CurrentHealth;
        m_sliderMana.value = Unit.CurrentMana;
        m_txtStarts.text = Unit.Level.ToString();
    }

    public void SetViewVisibility(bool isVisible)
    {
        m_view.SetActive(isVisible);
    }

    public ChessUnitRuntime Unit { get; private set; }
}
