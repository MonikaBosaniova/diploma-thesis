using UnityEngine;

namespace Games.CPU
{
    public class CPULevelController : LevelController
    {
        [SerializeField] private bool shieldActivated = false;
        [SerializeField] private bool shieldSentToRam = false;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            base.Init();
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            base.Close();
        }

        public void CheckFinishState()
        {
            Debug.Log("CallFinishState");
            if (shieldActivated && shieldSentToRam)
                StartCoroutine(WaitToShowCompleteLevel());
        }

        public void SetShieldActivated(bool sa)
        {
            shieldActivated = sa;
        }

        public void ShieldSentToRam(bool shieldSent)
        {
            shieldSentToRam = shieldSent;
        }
        
    }
}
