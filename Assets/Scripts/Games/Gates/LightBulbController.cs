using UnityEngine;

namespace Gates
{
    public class LightBulbController : NodeController 
    {
        public NodeController firstInput;
        public GameObject firstLine;

        public GameObject wire;
        
        void Start()
        {
            Initialize();
            firstInput.OnValueChanged += newValue => { 
                Value = newValue;
                SetLineValue(newValue, firstLine);
            };

        }

        public void HideWire()
        {
            wire.SetActive(false);
        }

        private void Initialize()
        {
            Value = firstInput.Value;
            SetLineValue(Value, firstLine);
        }
    }
}
