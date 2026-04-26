using UnityEngine;

namespace Gates
{
    /// <summary>
    /// Input switch node in the circuit, sets initial value on Awake
    /// </summary>
    public class SwitchController : NodeController
    {
        [Header("SET BEFORE PLAYING")]
        public bool InputValue = false;

        private void Awake()
        {
            Value = InputValue;
        }
    }
}
