using UnityEngine;
using UnityEngine.EventSystems;

namespace Games.Ram
{
    public class SnappedAddressCubieController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool snapped = false;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;
        
        MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!snapped) return;
            meshRenderer.material = highlightMaterial;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!snapped) return;
            meshRenderer.material = defaultMaterial;
        }
    }
}
