using UnityEngine;

public class ChessUnitHolderView : MonoBehaviour
{
    [SerializeField] private Transform m_modelPivot;

    public void Setup(ChessUnitRuntime unit)
    {
        Unit = unit;

        var modelInstance = Instantiate(unit.ChessUnitSO.ModelReferences.Model, m_modelPivot);
        modelInstance.transform.localPosition = Vector3.zero;
        modelInstance.transform.localRotation = Quaternion.identity;
        modelInstance.transform.localScale = Vector3.one;
    }

    public ChessUnitRuntime Unit { get; private set; }  
}
