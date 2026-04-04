using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SlotViewManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ObjectSelectionManager m_objectSelectionManager;

    [Header("Groups")]
    [SerializeField] private List<SlotViewManager> m_groupsManagers;

    [Header("Events")]
    public UnityEvent<ChessUnitHolderView> OnItemEnterGroup;
    public UnityEvent<ChessUnitHolderView> OnItemExitGroup;

    [SerializeField] private Transform m_slotViewParent;

    private SlotView m_oldSlotView;
    private List<SlotView> m_slotViews;
    private ChessUnitHolderView m_selectedItem;

    private bool m_needUpdate = false;

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

        BestSlotForSelectedItem = null;

        //Debug.Log("UpdateSlotViews");

        foreach (var slot in m_slotViews)
        {
            slot.UpdateItemInside();
            slot.SetHoverView(false);

            //Debug.Log($"    {slot.name} {slot.CurrentItemInside == m_selectedItem}");
            if (slot.CurrentItemInside != m_selectedItem) continue;
            if (slot.CurrentItem != null) continue;

            var dist = Vector3.Distance(slot.transform.position, slot.CurrentItemInside.transform.position);

            if (dist < minDist)
            {
                BestSlotForSelectedItem = slot;
                minDist = dist;
            }
        }

        //Debug.Log($"Best Item {BestSlotForSelectedItem}");

        if (BestSlotForSelectedItem != null)
        {
            BestSlotForSelectedItem.SetHoverView(true);
        }
    }

    private void HandleSlotViewHoverEnter(SlotView slotView, ChessUnitHolderView item)
    {
        if (m_selectedItem != item) return;

        m_needUpdate = true;
    }

    private void HandleSlotViewHoverExit(SlotView slotView, ChessUnitHolderView item)
    {
        if (m_selectedItem != item) return;

        m_needUpdate = true;
    }

    private void HandleObjectItemSelected(GameObject item)
    {
        m_selectedItem = item.GetComponent<ChessUnitHolderView>();

        m_slotViews.ForEach(slot =>
        {
            slot.UpdateItemInside();
            slot.SetCollision(true);
        });

        m_oldSlotView = m_slotViews.Find(slot => slot.CurrentItem == m_selectedItem);

        UpdateSlotsViews();
    }

    private void HandleObjectItemDeselected(GameObject item)
    {
        var unitHolder = item.GetComponent<ChessUnitHolderView>();

        //Debug.Log($"{name}");

        if (BestSlotForSelectedItem != null)
        {
            BestSlotForSelectedItem.SetItem(unitHolder);

            if (m_oldSlotView != null)
            {
                m_oldSlotView.RmvItem();
                //Debug.Log("   Continua no grupo");
            }
            else
            {
                OnItemEnterGroup?.Invoke(unitHolder);
                //Debug.Log("   Entrou no grupo");
            }
        }
        else
        {
            if (m_oldSlotView != null)
            {
                if (m_groupsManagers.All(manager => manager.BestSlotForSelectedItem == null))
                {
                    m_oldSlotView.SetItem(unitHolder);

                    //Debug.Log("   Estava aqui mas não foi para outro grupo ... voltando");
                }
                else
                {
                    m_oldSlotView.RmvItem();
                    OnItemExitGroup?.Invoke(unitHolder);

                    //Debug.Log("   Saiu do grupo");
                }
            }
            else
            {
                //Debug.Log("   Não entrou no group e nem estava aqui!");
            }
        }


        m_slotViews.ForEach(slot => { slot.SetCollision(false); slot.SetHoverView(false); }); 
    }

    public SlotView BestSlotForSelectedItem { get; private set; }

    public List<SlotView> Slots => m_slotViews;
    public List<ChessUnitHolderView> Items => m_slotViews.Where(sv => sv.CurrentItem != null).Select(sv => sv.CurrentItem).ToList();
}
