using UnityEngine;

public class ChessUnitHolderView : MonoBehaviour
{
    [SerializeField] private Transform m_modelPivot;
    [SerializeField] private ChessUnitStatusView m_unitStatusView;

    public void Setup(ChessUnitRuntime unit)
    {
        Unit = unit;

        var modelInstance = Instantiate(unit.ChessUnitSO.ModelReferences.Model, m_modelPivot);
        modelInstance.transform.localPosition = Vector3.zero;
        modelInstance.transform.localRotation = Quaternion.identity;
        modelInstance.transform.localScale = Vector3.one;

        m_unitStatusView.Setup(Unit);
    }

    public ChessUnitRuntime Unit { get; private set; }  
}
