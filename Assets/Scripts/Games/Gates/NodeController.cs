using System;
using UnityEngine;

namespace Gates
{
    public abstract class NodeController : MonoBehaviour
    {
        public event Action<bool> OnValueChanged;
        public GameObject TrueModel;
        public GameObject FalseModel;
        
        public Material FalseMaterial;
        public Material TrueMaterial;

        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                UpdateVisuals();
                OnValueChanged?.Invoke(_value);
            }
        }
        private bool _value;
        
        public void SwitchValue()
        {
            Value = !Value;
            UpdateVisuals();
            //Debug.Log("Switched to: " + Value);
        }

        private void UpdateVisuals()
        {
            if (TrueModel == null && FalseModel == null)
                return;
            if (Value)
            {
                TrueModel.SetActive(true);
                FalseModel.SetActive(false);
            }
            else
            {
                TrueModel.SetActive(false);
                FalseModel.SetActive(true);
            }
        }

        protected void CreateLines(GameObject from, GameObject to)
        {
        }

        protected void SetLineValue(bool value, GameObject line)
        {
            var allWireComponents = line.GetComponentsInChildren<MeshRenderer>();
            foreach (var wire in allWireComponents)
            {
                wire.sharedMaterial  = value ? TrueMaterial : FalseMaterial; 
            }
            //line.transform.GetChild(Convert.ToInt32(value)).gameObject.SetActive(true);
            //line.transform.GetChild(Convert.ToInt32(!value)).gameObject.SetActive(false);
        }
    }
}
