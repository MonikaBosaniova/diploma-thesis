using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Level controller for the CPU minigame, tracks shield activation and RAM state for completion
    /// </summary>
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

        /// <summary>
        /// Checks if both shield conditions are met and triggers level completion
        /// </summary>
        public void CheckFinishState()
        {
            if (shieldActivated && shieldSentToRam)
                StartCoroutine(WaitToShowCompleteLevel());
        }

        /// <summary>
        /// Sets the shield activated flag
        /// </summary>
        /// <param name="sa">True if the shield is activated</param>
        public void SetShieldActivated(bool sa)
        {
            shieldActivated = sa;
        }

        /// <summary>
        /// Sets the shield sent to RAM flag
        /// </summary>
        /// <param name="shieldSent">True if the shield was sent to RAM</param>
        public void ShieldSentToRam(bool shieldSent)
        {
            shieldSentToRam = shieldSent;
        }
        
    }
}
