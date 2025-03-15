using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gates
{
    public class LightBulbController : NodeController 
    {
        public LineRenderer lineRenderer;
        public NodeController firstInput;
        
        void Start()
        {
            Initialize();
            firstInput.OnValueChanged += newValue => { 
                Value = newValue;
            };

        }

        private void Initialize()
        {
            Value = firstInput.Value;
        }
    }
}
