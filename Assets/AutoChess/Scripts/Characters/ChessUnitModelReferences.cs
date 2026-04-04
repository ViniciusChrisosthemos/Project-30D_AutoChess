using UnityEngine;

public class ChessUnitModelReferences : MonoBehaviour
{
    [SerializeField] private GameObject m_model;
    [SerializeField] private Animator m_animator;

    public GameObject Model { get { return m_model; } }
    public Animator Animator => m_animator;
}
