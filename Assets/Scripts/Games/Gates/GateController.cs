using TMPro;
using UnityEngine;

namespace Gates
{
    public class GateController : NodeController
    {
        public TMP_Text textField;
        public GateTypes gateType;
        
        public NodeController firstInput;
        public NodeController secondInput;
        public LineRenderer firstLine;
        public LineRenderer secondLine;
        public LineRenderer straightLine;
        
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
                SetLineValue(newValue, firstLine);
            };
            secondInput.OnValueChanged += newValue =>
            {
                inputValue2 = newValue;
                Value = _gate.Evaluate(inputValue1, inputValue2);            
                SetLineValue(newValue, secondLine);
            };

        }

        private void Initialize()
        {
            switch (gateType)
            {
                case GateTypes.NOT:
                    _gate = new NotGate();
                    firstLine.enabled = false;
                    firstLine = straightLine;
                    secondLine.enabled = false;
                    break;
                case GateTypes.AND:
                    _gate = new AndGate();
                    straightLine.enabled = false;
                    break;
                case GateTypes.OR:
                    _gate = new OrGate();
                    straightLine.enabled = false;
                    break;
                case GateTypes.XOR:
                    _gate = new XorGate();
                    straightLine.enabled = false;
                    break;
                case GateTypes.None:
                    Debug.LogWarning("No gate type selected.");
                    break;
            }
            textField.text = gateType.ToString();
            inputValue1 = firstInput.Value;
            inputValue2 = secondInput.Value;
            Value = _gate.Evaluate(inputValue1, inputValue2);
            SetLineValue(firstInput.Value, firstLine);
            SetLineValue(secondInput.Value, secondLine);
        }
    }
}
