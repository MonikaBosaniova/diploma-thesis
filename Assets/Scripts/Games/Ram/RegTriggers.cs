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
            Debug.Log(other.name);
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

            collidingData._regParent = transform;
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
            data.transform.position = new Vector3(data._regParent.position.x, data.transform.position.y, data._regParent.position.z);
            
            if(data._regParent.childCount > 0 && data._regParent.GetChild(0) != data.transform) Destroy(data._regParent.GetChild(0).gameObject);
            data.transform.parent = data._regParent;
            RegTrigger regTrigger = data._regParent.GetComponent<RegTrigger>();
            regTrigger.snappedData = data;
            regTrigger.snap?.Invoke();
            regTrigger.SetHighlight(false);
        }
        
    }
}
