using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SlotViewManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ObjectSelectionManager m_objectSelectionManager;

    [Header("Group References")]
    [SerializeField] private List<SlotViewManager> m_groups;

    [Header("Events")]
    public UnityEvent<GameObject> OnItemExitGroup;
    public UnityEvent<GameObject> OnItemEnterGroup;

    [SerializeField] private Transform m_slotViewParent;

    private List<SlotView> m_slotViews;
    private SlotView m_slotOfItemSelected;
    private SlotView m_closestSlotView;

    private void Awake()
    {
        m_slotViews = m_slotViewParent.GetComponentsInChildren<SlotView>().ToList();

        foreach(var slotView in m_slotViews)
        {
            slotView.OnItemHoverEnter.AddListener(HandleSlotViewHoverEnter);
            slotView.OnItemHoverExit.AddListener(HandleSlotViewHoverExit);
        }

        m_objectSelectionManager.OnItemSelected.AddListener(HandleObjectItemSelected);
        m_objectSelectionManager.OnItemDeselected.AddListener(HandleObjectItemDeselected);

    }

    private void HandleSlotViewHoverEnter(SlotView slotView, GameObject item)
    {
        m_closestSlotView = slotView;
        var minDist = float.MaxValue;

        foreach (var slot in m_slotViews)
        {
            var dist = Vector3.Distance(slot.transform.position, item.transform.position);

            if (dist < minDist)
            {
                m_closestSlotView = slot;
                minDist = dist;
            }

            slot.SetHoverView(false);

            //Debug.Log($"    {minDist} {dist} {m_closestSlotView.name}");
        }

        m_closestSlotView.SetHoverView(true);
    }

    private void HandleSlotViewHoverExit(SlotView slotView, GameObject item)
    {
        foreach (var slot in m_slotViews)
        {
            if (slot.CurrentItemInside == null) slot.SetHoverView(false);
        }
    }

    private void HandleObjectItemSelected(GameObject item)
    {
        m_slotViews.ForEach(slot => slot.SetCollision(true));

        m_slotOfItemSelected = Slots.Find(slot => slot.CurrentItem == item);
    }

    private void HandleObjectItemDeselected(GameObject item)
    {
        var activeSlot = Slots.Find(slot => slot.CurrentItemInside == item);
        //var activeSlot = m_closestSlotView;

        if (activeSlot != null)
        {
            activeSlot.SetItem(item);

            if (m_slotOfItemSelected == null)
            {
                OnItemEnterGroup?.Invoke(item);
            }
            else
            {
                m_slotOfItemSelected.RmvItem();
            }
        }
        else if (m_slotOfItemSelected != null)
        {
            m_slotOfItemSelected.RmvItem();

            OnItemExitGroup?.Invoke(item);
        }

        m_slotViews.ForEach(slot => { slot.SetCollision(false); slot.SetHoverView(false); }); 
    }

    public List<SlotView> Slots => m_slotViews;
    public List<GameObject> Items => m_slotViews.Where(sv => sv.CurrentItem != null).Select(sv => sv.CurrentItem).ToList();
}
