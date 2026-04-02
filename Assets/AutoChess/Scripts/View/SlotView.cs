using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class SlotView : MonoBehaviour
{
    [SerializeField] private Transform m_slotPivot;
    [SerializeField] private GameObject m_slotHoverView;
    [SerializeField] private LayerMask m_itemTargetLayer;

    [Header("Events")]
    public UnityEvent<SlotView, ChessUnitHolderView> OnItemHoverEnter;
    public UnityEvent<SlotView, ChessUnitHolderView> OnItemHoverExit;

    private BoxCollider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        m_collider.isTrigger = true;

        SetHoverView(false);

        CurrentItemInside = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValid(other.gameObject))
        {
            CurrentItemInside = other.gameObject.GetComponent<ChessUnitHolderView>();
            OnItemHoverEnter?.Invoke(this, CurrentItemInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValid(other.gameObject))
        {
            CurrentItemInside = null;
            OnItemHoverExit?.Invoke(this, other.GetComponent<ChessUnitHolderView>());
        }
    }

    public void SetHoverView(bool isActive)
    {
        m_slotHoverView.SetActive(isActive);
    }

    public void SetItem(ChessUnitHolderView item)
    {
        CurrentItem = item;
        item.transform.position = m_slotPivot.position;
    }

    public void RmvItem()
    {
        CurrentItem = null;
    }

    public void SetCollision(bool isActive)
    {
        m_collider.enabled = isActive;
    }

    public void UpdateItemInside()
    {
        var collidersInside = Physics.OverlapBox(m_collider.bounds.center, m_collider.bounds.extents, m_collider.transform.rotation).ToList();

        if (!collidersInside.Any(collider => IsValid(collider.gameObject)))
        {
            CurrentItemInside = null;
        }
    }

    public bool IsValid(GameObject obj)
    {
        return m_itemTargetLayer.CompareLayers(obj.layer);
    }

    public bool IsHighlighted => m_slotHoverView.activeSelf;
    public ChessUnitHolderView CurrentItem { get; private set; }
    public ChessUnitHolderView CurrentItemInside { get; private set; }
}
