using UnityEngine;

namespace Gates
{
    /// <summary>
    /// Represents a light bulb node in the circuit, displays the output value visually
    /// </summary>
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

        /// <summary>
        /// Hides the connecting wire visual
        /// </summary>
        public void HideWire()
        {
            wire.SetActive(false);
        }

        /// <summary>
        /// Sets initial value and line visual from the input node
        /// </summary>
        private void Initialize()
        {
            Value = firstInput.Value;
            SetLineValue(Value, firstLine);
        }
    }
}
