using System;
using UnityEngine;

namespace Games.CPU
{
    public class RegTrigger : MonoBehaviour
    {
        [Header("Materials")] 
        [SerializeField] internal bool highlightingEnabled = true;
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
            Debug.Log(other.name);
            //other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
            other.transform.TryGetComponent(out draggableObject);
            if (draggableObject == null) return;
            //if (shapeController == null || draggableObject == null) return;
            
            if (draggableObject.dragging)
            {
                //if (!Snapped)
                //{
                    //other.transform.parent.gameObject.GetComponent<ShapeController>().AddBgTrigger(this);
                    if(highlightingEnabled)
                        meshRenderer.material = OnMaterial;
                // }
                // else
                // {
                //     if(highlightingEnabled)
                //         meshRenderer.material = AlreadySnappedMaterial;
                // }
            }
            else
            {
                if(highlightingEnabled)
                    meshRenderer.material = OffMaterial;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
            //if (shapeController != null)
            //{
                //other.transform.parent.gameObject.GetComponent<ShapeController>().RemoveBgTrigger(this);
                if(highlightingEnabled)
                    meshRenderer.material = OffMaterial;
            //}
        }

        public void SetSnapped(bool value)
        {
            Snapped = value;
        }

        public void ClearColoring()
        {
            if(highlightingEnabled)
                meshRenderer.material = OffMaterial;
        }

        public void SetHighlight(bool value)
        {
            if (highlightingEnabled)
                meshRenderer.material = value ? OnMaterial : OffMaterial;
        }
        
    }
}
