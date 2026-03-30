using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChessUnitModelReferences : MonoBehaviour
{
    [SerializeField] private GameObject m_model;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public GameObject Model { get { return m_model; } }
    public Animator Animator {  get; private set; }
}
