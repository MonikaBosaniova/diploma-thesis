using System;
using UnityEngine;

namespace Gates
{
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
