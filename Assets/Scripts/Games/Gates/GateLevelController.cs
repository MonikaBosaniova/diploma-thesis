using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gates
{
    public class GateLevelController : LevelController
    {
        private List<LightBulbController> _allLightBulbs;
        
        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            //transform.GetChild(0).gameObject.SetActive(true);
            _allLightBulbs = gameObject.GetComponentsInChildren<LightBulbController>().ToList();

            foreach (var lightBulb in _allLightBulbs)
            {
                lightBulb.OnValueChanged += CheckFinishState;
            }
            
            base.Init();
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            //transform.GetChild(0).gameObject.SetActive(false);
            base.Close();
        }

        private void OnDisable()
        {
            if(_allLightBulbs == null)
                return;
            
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
            
            StartCoroutine(WaitToShowCompleteLevel());
        }
        

    }
}
