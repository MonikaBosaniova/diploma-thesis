using UnityEngine;

namespace Games.HddSsd
{
    public class HddSsdLevelController : LevelController
    {
        HddSsdGameManager ramGameManager;
        
        public override void Init()
        {
            ramGameManager = FindFirstObjectByType<HddSsdGameManager>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            base.Init();
        }
        

        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }
        
    }
}
