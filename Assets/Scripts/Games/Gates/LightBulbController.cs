using UnityEngine;

namespace Gates
{
    public class LightBulbController : NodeController 
    {
        public NodeController firstInput;
        public GameObject firstLine;
        
        void Start()
        {
            Initialize();
            firstInput.OnValueChanged += newValue => { 
                Value = newValue;
                SetLineValue(newValue, firstLine);
            };

        }

        private void Initialize()
        {
            Value = firstInput.Value;
            SetLineValue(Value, firstLine);
        }
    }
}
