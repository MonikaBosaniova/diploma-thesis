using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CPU
{
    public class CPULevelController : LevelController
    {
        
        public override void Init()
        {
            
            base.Init();
        }

        private void Update()
        {
            
        }
        
        
        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }
        
        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
        
    }
}
