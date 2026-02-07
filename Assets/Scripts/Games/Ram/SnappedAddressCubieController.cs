using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class SnappedAddressCubieController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] internal bool canBeDestroyed = false;
        [SerializeField] internal bool snapped = false;
        [SerializeField] private float timeToLive = 0f;
        [SerializeField] private GameObject hologramCube;
        
        [Header("Position")]
        [SerializeField] private int rowPosition;
        [SerializeField] private int columnPosition;
        
        [Header("Highlighting")]
        public bool enableHighlighting = false;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;

        internal BgCubeTrigger triggerWhereCubieIsSnapped;

        private Transform CPUPoint;
        
        MeshRenderer meshRenderer;
        DraggableObject draggableObject;
        Vector3 cubieSnappedPosition = Vector3.zero;
        private GameObject cubeHologram;

        private void Start()
        {
            meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            timeToLive = Random.Range(5, 20);
            StartCoroutine(SetDestroyStateAfterTime());
            draggableObject = gameObject.GetComponent<DraggableObject>();
            draggableObject.DragStart += SpawnHologram;
            draggableObject.DragEnd += Snap; 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter: R-" + rowPosition + ", C-" + columnPosition);
            if (!enableHighlighting) return;
            meshRenderer.material = highlightMaterial;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!enableHighlighting) return;
            meshRenderer.material = defaultMaterial;
        }

        public bool EnableHighlighting
        {
            get => enableHighlighting;
            set => enableHighlighting = value;
        }
        
        public void SetPositionAndTrigger(float row, float column, BgCubeTrigger trigger)
        {
            cubieSnappedPosition = transform.position;
            rowPosition = Mathf.RoundToInt(row);
            columnPosition = Mathf.RoundToInt(column);
            triggerWhereCubieIsSnapped = trigger;
        }

        public Tuple<int, int> GetIfCanBeDestroyedAddress()
        {
            return (canBeDestroyed && snapped) ? new Tuple<int, int>(rowPosition, columnPosition) : null;
        }
        
        public Tuple<int, int> GetAddress()
        {
            return new Tuple<int, int>(rowPosition, columnPosition);
        }

        internal void SetPositionOfTransform(Vector3 pos)
        {
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }

        internal void SetCPUPoint(Transform cpuPoint)
        {
            CPUPoint = cpuPoint;
        }

        internal void RemoveCPUPoint()
        {
            CPUPoint = null;
        }
        
        internal void Snap()
        {
            if (CPUPoint != null)
                CPUPoint.GetComponent<CPUWantedAddressController>().CheckAddress(this);
            SetPositionOfTransform(CPUPoint != null ? CPUPoint.transform.position : cubieSnappedPosition);
            if(CPUPoint == null) Destroy(cubeHologram);
        }
        
        private void SpawnHologram()
        {
            cubeHologram = Instantiate(hologramCube, cubieSnappedPosition, Quaternion.identity);
        }
        
        private IEnumerator SetDestroyStateAfterTime() {
            yield return new WaitForSeconds(timeToLive);
            canBeDestroyed = true;
        }

        private void OnDestroy()
        {
            if(triggerWhereCubieIsSnapped != null)
                triggerWhereCubieIsSnapped.SetSnapped(false);
        }
    }
}
