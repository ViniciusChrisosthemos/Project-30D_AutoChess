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
    public UnityEvent<SlotView, GameObject> OnItemHoverEnter;
    public UnityEvent<SlotView, GameObject> OnItemHoverExit;

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
        var objectLayer = other.gameObject.layer;

        if (m_itemTargetLayer.CompareLayers(objectLayer))
        {
            CurrentItemInside = other.gameObject;
            OnItemHoverEnter?.Invoke(this, CurrentItemInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var objectLayer = other.gameObject.layer;

        if (m_itemTargetLayer.CompareLayers(objectLayer))
        {
            CurrentItemInside = null;
            OnItemHoverExit?.Invoke(this, other.gameObject);
        }
    }

    public void SetHoverView(bool isActive)
    {
        m_slotHoverView.SetActive(isActive);
    }

    public void SetItem(GameObject item)
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

        if (!collidersInside.Any(collider => collider.gameObject == CurrentItemInside))
        {
            CurrentItemInside = null;
        }
    }

    public bool IsHighlighted => m_slotHoverView.activeSelf;
    public GameObject CurrentItem { get; private set; }
    public GameObject CurrentItemInside { get; private set; }
}
