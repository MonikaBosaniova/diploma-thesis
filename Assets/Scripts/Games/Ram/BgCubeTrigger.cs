using System;
using UnityEngine;

public class BgCubeTrigger : MonoBehaviour
{
    [Header("Materials")] 
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;
    [SerializeField] Material AlreadySnappedMaterial;

    [Header ("---DEBUG---")]
    [SerializeField] bool Snapped;
    MeshRenderer meshRenderer;
    DraggableObject draggableObject;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        other.transform.parent.TryGetComponent(out draggableObject);
        if (shapeController == null || draggableObject == null) return;
        
        if (draggableObject.dragging)
        {
            if (!Snapped)
            {
                other.transform.parent.gameObject.GetComponent<ShapeController>().AddBgTrigger(this);
                meshRenderer.material = OnMaterial;
            }
            else
            {
                meshRenderer.material = AlreadySnappedMaterial;
            }
        }
        else
        {
            meshRenderer.material = OffMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        if (shapeController != null)
        {
            other.transform.parent.gameObject.GetComponent<ShapeController>().RemoveBgTrigger(this);
            meshRenderer.material = OffMaterial;
        }
    }

    public void SetSnapped(bool value)
    {
        Snapped = value;
    }

    public void ClearColoring()
    {
        meshRenderer.material = OffMaterial;
    }
    
}
