using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotViewManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ObjectSelectionManager m_objectSelectionManager;

    [SerializeField] private Transform m_slotViewParent;

    private List<SlotView> m_slotViews;
    private SlotView m_currentSlotViewHover;

    private void Awake()
    {
        m_slotViews = m_slotViewParent.GetComponentsInChildren<SlotView>().ToList();

        foreach(var slotView in m_slotViews)
        {
            slotView.OnItemHoverEnter.AddListener(HandleSlotViewHoverEnter);
            slotView.OnItemHoverExit.AddListener(HandleSlotViewHoverExit);
        }

        m_objectSelectionManager.OnItemSelected.AddListener(HabdleObjectItemSelected);
        m_objectSelectionManager.OnItemDeselected.AddListener(HandleObjectItemDeselected);
    }

    private void HandleSlotViewHoverEnter(SlotView slotView, GameObject item)
    {
        if (m_currentSlotViewHover != null && m_currentSlotViewHover != slotView)
        {
            m_currentSlotViewHover.SetHoverView(false);
        }

        m_currentSlotViewHover = slotView;
        m_currentSlotViewHover.SetHoverView(true);
    }

    private void HandleSlotViewHoverExit(SlotView slotView, GameObject item)
    {
        slotView.SetHoverView(false);

        var secondSlotViewSelected = m_slotViews.Find(slot => slot.CurrentItemInside == item);

        if (secondSlotViewSelected != null)
        {
            secondSlotViewSelected.SetHoverView(true);
            m_currentSlotViewHover = secondSlotViewSelected;
        }
        else
        {
            m_currentSlotViewHover = null;
        }
    }

    private void HabdleObjectItemSelected(GameObject item)
    {
        m_slotViews.ForEach(slot => slot.SetCollision(true));
    }

    private void HandleObjectItemDeselected(GameObject item)
    {
        if (m_currentSlotViewHover != null)
        {
            m_currentSlotViewHover.SetItem(item);
        }

        m_currentSlotViewHover = null;

        m_slotViews.ForEach(slot => { slot.SetCollision(false); slot.SetHoverView(false); }); 
    }

    public List<GameObject> Items => m_slotViews.Where(sv => sv.CurrentItem != null).Select(sv => sv.CurrentItem).ToList();
}
