using UnityEngine;
using UnityEngine.Serialization;

public class BgCubeTrigger : MonoBehaviour
{
    [Header("Materials")] 
    [SerializeField] internal bool highlightingEnabled = true;
    [FormerlySerializedAs("OnMaterial")] [SerializeField] private Material onMaterial;
    [FormerlySerializedAs("OffMaterial")] [SerializeField] private Material offMaterial;
    [FormerlySerializedAs("AlreadySnappedMaterial")] [SerializeField] private Material alreadySnappedMaterial;

    [FormerlySerializedAs("Snapped")]
    [Header ("---DEBUG---")]
    [SerializeField] private bool snapped;
    private MeshRenderer _meshRenderer;
    private DraggableObject _draggableObject;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        other.transform.parent.TryGetComponent(out _draggableObject);
        if (shapeController == null || _draggableObject == null) return;
        
        if (_draggableObject.dragging)
        {
            if (!snapped)
            {
                other.transform.parent.gameObject.GetComponent<ShapeController>().AddBgTrigger(this);
                if(highlightingEnabled)
                    _meshRenderer.material = onMaterial;
            }
            else
            {
                if(highlightingEnabled)
                    _meshRenderer.material = alreadySnappedMaterial;
            }
        }
        else
        {
            if(highlightingEnabled)
                _meshRenderer.material = offMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        if (shapeController != null)
        {
            other.transform.parent.gameObject.GetComponent<ShapeController>().RemoveBgTrigger(this);
            if(highlightingEnabled)
                _meshRenderer.material = offMaterial;
        }
    }
    
    /// <summary>
    /// Sets if the trigger has the snapped cube "inside"
    /// </summary>
    /// <param name="value"></param>
    public void SetSnapped(bool value)
    {
        snapped = value;
    }
    
    /// <summary>
    /// Sets offMaterial
    /// </summary>
    public void ClearColoring()
    {
        if(highlightingEnabled)
            _meshRenderer.material = offMaterial;
    }
    
    /// <summary>
    /// Sets highlighting material depended on input value
    /// </summary>
    /// <param name="value">TRUE -> onMaterial, FALSE -> offMaterial</param>
    public void SetHighlight(bool value)
    {
        if (highlightingEnabled)
            _meshRenderer.material = value ? onMaterial : offMaterial;
    }
    
}
