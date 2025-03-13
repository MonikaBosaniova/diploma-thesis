using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gates
{
    public class GateController : NodeController 
    {
        public GateTypes gateType;
        public NodeController firstInput;
        public NodeController secondInput;
        
        [Header("-----READ ONLY-----")]
        public bool inputValue1;
        public bool inputValue2;

        private ILogicGate _gate;
        
        void Start()
        {
            Initialize();
            firstInput.OnValueChanged += newValue => { 
                inputValue1 = newValue;
                Value = _gate.Evaluate(inputValue1, inputValue2);
            };
            secondInput.OnValueChanged += newValue =>
            {
                inputValue2 = newValue;
                Value = _gate.Evaluate(inputValue1, inputValue2);
            };

        }

        private void Initialize()
        {
            switch (gateType)
            {
                case GateTypes.Not:
                    _gate = new NotGate();
                    break;
                case GateTypes.And:
                    _gate = new AndGate();
                    break;
                case GateTypes.Or:
                    _gate = new OrGate();
                    break;
                case GateTypes.Xor:
                    _gate = new XorGate();
                    break;
                case GateTypes.None:
                    Debug.LogWarning("No gate type selected.");
                    break;
            }
            inputValue1 = firstInput.Value;
            inputValue2 = secondInput.Value;
            Value = _gate.Evaluate(inputValue1, inputValue2);
        }
    }
}
