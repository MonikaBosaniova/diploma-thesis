using System;
using UnityEngine;


namespace Gates
{
    public abstract class NodeController : MonoBehaviour
    {
        public event Action<bool> OnValueChanged;
        public GameObject TrueModel;
        public GameObject FalseModel;

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
            Debug.Log("Switched to: " + Value);
        }

        private void UpdateVisuals()
        {
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

        protected void SetLineValue(bool value, LineRenderer line)
        {
            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            if (value)
            {
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
                
            }
            else
            {
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
            }
            line.colorGradient = gradient;
        }
    }
}
