using TMPro;
using UnityEngine;

namespace Gates
{
    public class GateController : NodeController
    {
        public GateTypes gateType;
        
        public NodeController firstInput;
        public NodeController secondInput;
        public GameObject firstLine;
        public GameObject secondLine;
        public GameObject straightLine;
        
        [Header("-----READ ONLY-----")]
        public Transform allGates;
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
                    firstLine.SetActive(false);;
                    firstLine = straightLine;
                    secondLine.SetActive(false);;
                    break;
                case GateTypes.AND:
                    _gate = new AndGate();
                    straightLine.SetActive(false);;
                    break;
                case GateTypes.OR:
                    _gate = new OrGate();
                    straightLine.SetActive(false);;
                    break;
                case GateTypes.XOR:
                    _gate = new XorGate();
                    straightLine.SetActive(false);;
                    break;
                case GateTypes.None:
                    Debug.LogWarning("No gate type selected.");
                    break;
            }

            allGates = transform.GetChild(0);
            allGates.GetChild((int)gateType).gameObject.SetActive(true);
            inputValue1 = firstInput.Value;
            inputValue2 = secondInput.Value;
            Value = _gate.Evaluate(inputValue1, inputValue2);
            SetLineValue(firstInput.Value, firstLine);
            SetLineValue(secondInput.Value, secondLine);
        }
    }
}
