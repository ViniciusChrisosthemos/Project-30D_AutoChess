using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class SlotViewManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ObjectSelectionManager m_objectSelectionManager;

    [Header("Events")]
    public UnityEvent<GameObject> OnItemExitGroup;
    public UnityEvent<GameObject> OnItemEnterGroup;

    [SerializeField] private Transform m_slotViewParent;

    private List<SlotView> m_slotViews;
    private List<GameObject> m_itensInsideGroup;
    private SlotView m_closestSlotView;

    private bool m_needUpdate = false;

    private void Awake()
    {
        m_itensInsideGroup = new List<GameObject>();
        m_slotViews = m_slotViewParent.GetComponentsInChildren<SlotView>().ToList();

        foreach(var slotView in m_slotViews)
        {
            slotView.OnItemHoverEnter.AddListener(HandleSlotViewHoverEnter);
            slotView.OnItemHoverExit.AddListener(HandleSlotViewHoverExit);
        }

        m_objectSelectionManager.OnItemSelected.AddListener(HandleObjectItemSelected);
        m_objectSelectionManager.OnItemDeselected.AddListener(HandleObjectItemDeselected);

    }

    private void LateUpdate()
    {
        if (m_needUpdate)
        {
            UpdateSlotsViews();

            m_needUpdate = false;
        }
    }

    private void UpdateSlotsViews()
    {
        var minDist = float.MaxValue;

        m_closestSlotView = null;

        foreach (var slot in m_slotViews)
        {
            slot.UpdateItemInside();
            slot.SetHoverView(false);

            if (slot.CurrentItemInside == null) continue;

            var dist = Vector3.Distance(slot.transform.position, slot.CurrentItemInside.transform.position);

            if (dist < minDist)
            {
                m_closestSlotView = slot;
                minDist = dist;
            }

            //Debug.Log($"    {minDist} {dist} {m_closestSlotView.name}");
        }

        if (m_closestSlotView != null)
        {
            m_closestSlotView.SetHoverView(true);
        }

        /*
        Debug.Log("Update Views");

        foreach (var slot in m_slotViews)
        {
            Debug.Log($"   {slot.name}={slot.CurrentItemInside != null} | Highlight On={slot.IsHighlighted}");
        }

        Debug.Log($"Selected View {m_closestSlotView}");*/
    }

    private void HandleSlotViewHoverEnter(SlotView slotView, GameObject item)
    {
        m_needUpdate = true;
    }

    private void HandleSlotViewHoverExit(SlotView slotView, GameObject item)
    {
        m_needUpdate = true;
    }

    private void HandleObjectItemSelected(GameObject item)
    {
        m_slotViews.ForEach(slot => slot.SetCollision(true));

        UpdateSlotsViews();
    }

    private void HandleObjectItemDeselected(GameObject item)
    {
        //var activeSlot = m_closestSlotView;

        if (m_closestSlotView != null)
        {
            m_closestSlotView.SetItem(item);

            if (!m_itensInsideGroup.Contains(item))
            {
                m_itensInsideGroup.Add(item);
                OnItemEnterGroup?.Invoke(item);
            }
        }
        else if (m_itensInsideGroup.Contains(item))
        {
            foreach(var slot in m_slotViews)
            {
                if (slot.CurrentItem == item) slot.RmvItem();

                break;
            }

            m_itensInsideGroup.Remove(item);
            OnItemExitGroup?.Invoke(item);
        }

        m_slotViews.ForEach(slot => { slot.SetCollision(false); slot.SetHoverView(false); }); 
    }

    public List<SlotView> Slots => m_slotViews;
    public List<GameObject> Items => m_slotViews.Where(sv => sv.CurrentItem != null).Select(sv => sv.CurrentItem).ToList();
}
