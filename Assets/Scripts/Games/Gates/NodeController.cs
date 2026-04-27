using System;
using UnityEngine;

namespace Gates
{
    /// <summary>
    /// Abstract base class for all circuit nodes (switches, gates, light bulbs)
    /// Manages boolean value, visual models and wire material updates
    /// </summary>
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
        
        /// <summary>
        /// Toggles the node value between true and false
        /// </summary>
        public void SwitchValue()
        {
            Value = !Value;
            UpdateVisuals();
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

        /// <summary>
        /// Updates wire material based on the current value
        /// </summary>
        /// <param name="value">True for active material, false for inactive</param>
        /// <param name="line">The wire GameObject to update</param>
        protected void SetLineValue(bool value, GameObject line)
        {
            var allWireComponents = line.GetComponentsInChildren<MeshRenderer>();
            foreach (var wire in allWireComponents)
            {
                wire.sharedMaterial  = value ? TrueMaterial : FalseMaterial; 
            }
        }
    }
}
