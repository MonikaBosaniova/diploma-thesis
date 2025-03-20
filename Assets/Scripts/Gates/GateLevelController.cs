using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gates
{
    public class GateLevelController : LevelController
    {
        private List<LightBulbController> _allLightBulbs;
        
        private void OnEnable()
        {
            _allLightBulbs = gameObject.GetComponentsInChildren<LightBulbController>().ToList();

            foreach (var lightBulb in _allLightBulbs)
            {
                lightBulb.OnValueChanged += CheckFinishState;
            }
        }

        private void OnDisable()
        {
            foreach (var lightBulb in _allLightBulbs)
            {
                lightBulb.OnValueChanged -= CheckFinishState;
            }
        }

        private void CheckFinishState(bool b)
        {
            if (_allLightBulbs.Count <= 0) return;

            if (_allLightBulbs.Any(lightBulb => !lightBulb.Value))
            {
                return;
            }

            InvokeOnLevelEnded();
        }
    }
}
