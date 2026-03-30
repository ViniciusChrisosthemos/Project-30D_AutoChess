using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ObjectSelectionManager : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private LayerMask m_targetLayer;
    [SerializeField] private LayerMask m_floorLayer;

    [Header("Events")]
    public UnityEvent<GameObject> OnItemSelected;
    public UnityEvent<GameObject> OnItemDeselected;

    private GameObject m_currentObjectSelected;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if(Physics.Raycast(ray, out var hitInfo, 1000, m_targetLayer))
            {
                m_currentObjectSelected = hitInfo.collider.gameObject;
                OnItemSelected?.Invoke(m_currentObjectSelected);
            }
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (m_currentObjectSelected != null)
                OnItemDeselected?.Invoke(m_currentObjectSelected);

            m_currentObjectSelected = null;
        }

        if (m_currentObjectSelected != null)
        {
            var ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hitInfo, 1000, m_floorLayer))
            {
                m_currentObjectSelected.transform.position = hitInfo.point;
            }
        }
    }
}
