using System;
using UnityEngine;

public class BgCubeTrigger : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;
    
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
        if (shapeController != null)
        {
            other.transform.parent.gameObject.GetComponent<ShapeController>().AddBgTrigger(this);
            meshRenderer.material = OnMaterial;
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
}
