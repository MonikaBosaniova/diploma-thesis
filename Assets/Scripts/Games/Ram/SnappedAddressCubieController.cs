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
        
        [Header("Position")]
        [SerializeField] private int rowPosition;
        [SerializeField] private int columnPosition;
        
        [Header("Highlighting")]
        public bool enableHighlighting = false;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;
        
        MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            timeToLive = Random.Range(5, 20);
            StartCoroutine(SetDestroyStateAfterTime());
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
        
        public void SetPosition(float row, float column)
        {
            rowPosition = Mathf.RoundToInt(row);
            columnPosition = Mathf.RoundToInt(column);
        }

        public Tuple<int, int> GetIfCanBeDestroyedAddress()
        {
            return canBeDestroyed ? new Tuple<int, int>(rowPosition, columnPosition) : null;
        }
        
        public Tuple<int, int> GetAddress()
        {
            return new Tuple<int, int>(rowPosition, columnPosition);
        }
        
        private IEnumerator SetDestroyStateAfterTime() {
            yield return new WaitForSeconds(timeToLive);
            canBeDestroyed = true;
        }
    }
}
