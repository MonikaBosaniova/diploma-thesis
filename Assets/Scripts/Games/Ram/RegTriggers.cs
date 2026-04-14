using System;
using UnityEngine;

namespace Games.CPU
{
    public class RegTrigger : MonoBehaviour
    {
        public RegData snappedData;
        
        [Header("Materials")] 
        [SerializeField] internal bool highlightingEnabled = true;
        [SerializeField] Material OnMaterial;
        [SerializeField] Material OffMaterial;
        [SerializeField] Material AlreadySnappedMaterial;

        [Header ("---DEBUG---")]
        [SerializeField] bool Snapped;
        MeshRenderer meshRenderer;
        DraggableObject draggableObject;

        internal Action snap;
        private RegData collidingData;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (transform.childCount == 0)
            {
                snappedData = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //other.transform.parent.TryGetComponent<ShapeController>(out var shapeController);
            other.transform.TryGetComponent(out draggableObject);
            other.transform.TryGetComponent(out collidingData);
            if (draggableObject == null || collidingData == null) return;
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

            collidingData.regParent = transform;
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
        
        internal void SnapDataToReg(RegData data)
        {
            data.transform.position = new Vector3(data.regParent.position.x, data.transform.position.y, data.regParent.position.z);
            
            if(data.regParent.childCount > 0 && data.regParent.GetChild(0) != data.transform) Destroy(data.regParent.GetChild(0).gameObject);
            data.transform.parent = data.regParent;
            RegTrigger regTrigger = data.regParent.GetComponent<RegTrigger>();
            regTrigger.snappedData = data;
            regTrigger.snap?.Invoke();
            regTrigger.SetHighlight(false);
        }
        
    }
}
