using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gates
{
    public class LightBulbController : NodeController 
    {
        public NodeController firstInput;
        public LineRenderer firstLine;
        
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
