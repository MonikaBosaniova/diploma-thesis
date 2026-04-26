using UnityEngine;

namespace Games.Hdd_Ssd
{
    /// <summary>
    /// Represents a single SSD memory segment with on/off visual states
    /// </summary>
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
