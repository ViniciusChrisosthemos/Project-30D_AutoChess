using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIShopView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button m_btnRefreshShop;
    [SerializeField] private Button m_btnLevel;
    [SerializeField] private UIListDisplay m_unitListDisplay;

    private ShopManager m_shopManager;
    private List<ShopUnitItem> m_shopUnitItems;

    private void Start()
    {
        m_shopManager = GameManager.Instance.ShopManager;

        m_btnLevel.onClick.AddListener(BuyLevel);
        m_btnRefreshShop.onClick.AddListener(BuyRefresh);

        m_unitListDisplay.SetItems(m_shopManager.AvailableUnits, HandleUnitSelected);
        m_shopUnitItems = m_unitListDisplay.GetItems().Select(c => c as ShopUnitItem).ToList();

        UpdateGoldOptions();
    }

    private void UpdateUnitList()
    {
        for (int i = 0; i < m_shopUnitItems.Count; i++)
        {
            m_shopUnitItems[i].SetItem(m_shopManager.AvailableUnits[i]);
        }
    }

    private void UpdateGoldOptions()
    {
        foreach (var shopItemView in m_shopUnitItems)
        {
            if (shopItemView.Unit == null) continue;

            shopItemView.SetInteractable(m_shopManager.CanBuyCharacter(shopItemView.Unit));
        }

        m_btnLevel.interactable = m_shopManager.CanBuyLevel();
        m_btnRefreshShop.interactable = m_shopManager.CanBuyRefresh();
    }

    private void HandleUnitSelected(UIItemController controller)
    {
        var shopItemView = controller as ShopUnitItem;

        m_shopManager.BuyCharacter(shopItemView.Unit);
        shopItemView.SetAvailable(false);

        UpdateGoldOptions();
    }

    public void BuyRefresh()
    {
        m_shopManager.BuyRefresh();

        UpdateUnitList();
        UpdateGoldOptions();
    }

    public void BuyLevel()
    {
        m_shopManager.BuyRefresh();

        UpdateGoldOptions();
    }
}
