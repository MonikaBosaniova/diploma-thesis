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
    bool newlySnapped;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        if (shapeController == null) return;
        
        if (!Snapped)
        {
            other.transform.parent.gameObject.GetComponent<ShapeController>().AddBgTrigger(this);
            meshRenderer.material = OnMaterial;
        }
        else
        {
            if (newlySnapped)
            {
                newlySnapped = false;
                meshRenderer.material = OffMaterial;
            }
            meshRenderer.material = AlreadySnappedMaterial;
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
        newlySnapped = value;
    }

    public void ClearColoring()
    {
        meshRenderer.material = OffMaterial;
    }
    
}
