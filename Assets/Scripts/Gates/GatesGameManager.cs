using System.Collections.Generic;
using System.Linq;

namespace Gates
{
    public class GatesGameManager : GameManager
    {
        protected override void Initialization()
        {
            base.Initialization();
            Levels = new List<LevelController>(gameObject.GetComponentsInChildren<GateLevelController>().ToList());
            foreach (var gateLevel in Levels.Cast<GateLevelController>())
            {
                gateLevel.OnLevelEnded += ContinueToNextLevel;
                gateLevel.gameObject.SetActive(false);
            }
            Levels.ElementAt(0).gameObject.SetActive(true);
        }
        
    }
}
