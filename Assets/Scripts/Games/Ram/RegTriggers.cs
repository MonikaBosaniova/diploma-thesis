using System;
using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Trigger zone for CPU register slots, handles data snapping, highlighting and collision detection
    /// </summary>
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

        /// <summary>
        /// Sets the snapped state of the register trigger
        /// </summary>
        /// <param name="value">Whether a data object is snapped</param>
        public void SetSnapped(bool value)
        {
            Snapped = value;
        }

        /// <summary>
        /// Resets the highlighting material to the off state
        /// </summary>
        public void ClearColoring()
        {
            if(highlightingEnabled)
                meshRenderer.material = OffMaterial;
        }

        /// <summary>
        /// Sets the highlight material based on the input value
        /// </summary>
        /// <param name="value">True for highlighted, false for default</param>
        public void SetHighlight(bool value)
        {
            if (highlightingEnabled)
                meshRenderer.material = value ? OnMaterial : OffMaterial;
        }
        
        /// <summary>
        /// Snaps a RegData object to its assigned register position
        /// </summary>
        /// <param name="data">The data to snap to the register</param>
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
