using UnityEngine;

namespace Games.HddSsd
{
    public class SsdSegment : MonoBehaviour
    {
        [SerializeField] private GameObject onModel;
        [SerializeField] private GameObject offModel;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            offModel.SetActive(true);
            onModel.SetActive(false);
        }

        public void SetValue(bool value)
        {
            onModel.SetActive(value);
            offModel.SetActive(!value);
        }
    }
}
