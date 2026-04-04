using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIShopView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Dependencies")]
    [SerializeField] private ObjectSelectionManager m_selectionManager;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_txtBoardExp;
    [SerializeField] private Button m_btnRefreshShop;
    [SerializeField] private Button m_btnLevel;
    [SerializeField] private UIListDisplay m_unitListDisplay;

    [Header("Sell Unit References")]
    [SerializeField] private GameObject m_sellUnitView;
    [SerializeField] private LayerMask m_sellAreaLayer;

    private bool m_isPointerInShopView;
    private GameState m_gameState;
    private ShopManager m_shopManager;
    private List<ShopUnitItem> m_shopUnitItems;
    private ChessUnitHolderView m_unitHolderSelected;

    private void Awake()
    {
        GameManager.Instance.OnGameContextReady.AddListener(Setup);

        m_btnLevel.onClick.AddListener(BuyLevel);
        m_btnRefreshShop.onClick.AddListener(BuyRefresh);

        m_selectionManager.OnItemSelected.AddListener(HandleItemSelected);
        m_selectionManager.OnItemDeselected.AddListener(HandleItemDeselected);

        m_sellUnitView.SetActive(false);
    }

    private void Update()
    {
        if (m_unitHolderSelected != null)
        {
            HandleSellUnit();
        }
    }

    public void Setup(GameContext gameContext)
    {
        m_gameState = gameContext.State;
        m_shopManager = gameContext.Services.ShopManager;

        m_unitListDisplay.SetItems(m_shopManager.AvailableUnits, HandleShopUnitSelected);
        m_shopUnitItems = m_unitListDisplay.GetItems().Select(c => c as ShopUnitItem).ToList();

        m_gameState.OnGoldChanged += _ => UpdateGoldOptions();
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

        var boardExperience = m_gameState.BoardState.Experience;
        var boardExperienceForNextLevel = m_gameState.BoardState.ExperienceForNextLevel;
        
        m_txtBoardExp.text = $"{boardExperience}/{boardExperienceForNextLevel}";
    }

    private void HandleShopUnitSelected(UIItemController controller)
    {
        var shopItemView = controller as ShopUnitItem;

        var buyOk = m_shopManager.BuyCharacter(shopItemView.Unit);
        
        if (buyOk)
        {
            shopItemView.SetAvailable(false);
        }
        else
        {
            Debug.Log("Erro ao tentar comprar unit");
        }
    }

    public void BuyRefresh()
    {
        m_shopManager.BuyRefresh();

        UpdateUnitList();
    }

    public void BuyLevel()
    {
        m_shopManager.BuyLevel();
    }

    private void HandleSellUnit()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (m_isPointerInShopView)
        {
            if (!m_sellUnitView.activeSelf) m_sellUnitView.SetActive(true);
        }
        else
        {
            if (m_sellUnitView.activeSelf) m_sellUnitView.SetActive(false);
        }
    }

    private void HandleItemSelected(GameObject unitGameObject)
    {
        m_unitHolderSelected = unitGameObject.GetComponent<ChessUnitHolderView>();
    }

    private void HandleItemDeselected(GameObject unitGameObject)
    {
        //Debug.Log($"==> {unitGameObject} - {m_unitHolderSelected}");

        if (m_isPointerInShopView)
        {
            m_shopManager.SellUnit(m_unitHolderSelected.Unit);
        }

        m_unitHolderSelected = null;

        m_sellUnitView.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isPointerInShopView = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_isPointerInShopView = false;
    }
}
