using UnityEngine;
using UnityEngine.UI;

public class UIShopView : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ShopManager m_manager;

    [Header("References")]
    [SerializeField] private Button m_btnRefreshShop;
    [SerializeField] private Button m_btnLevel;
    [SerializeField] private UIListDisplay m_unitListDisplay;


}
