using System.Collections.Generic;
using System.Linq;

namespace Gates
{
    /// <summary>
    /// Level controller for the logic gates minigame, checks completion when all light bulbs are on
    /// </summary>
    public class GateLevelController : LevelController
    {
        public bool freeMode = false;
        private List<LightBulbController> _allLightBulbs;
        
        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            //transform.GetChild(0).gameObject.SetActive(true);
            _allLightBulbs = gameObject.GetComponentsInChildren<LightBulbController>().ToList();
            
            if(!freeMode)
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

        /// <summary>
        /// Checks if all light bulbs have true value and triggers level completion
        /// </summary>
        /// <param name="b">New value of the changed light bulb (unused)</param>
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
