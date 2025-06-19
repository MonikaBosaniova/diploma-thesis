using System.Collections.Generic;
using System.Linq;
using GameStateMachine;

namespace Gates
{
    public class GatesGameManager : GameManager
    {
        protected override void Initialization()
        {
            base.Initialization();
            //= new List<LevelController>(TutorialParent.GetComponents<GateLevelController>().ToList());
            //Levels = new List<LevelController>(LevelsParent.GetComponents<GateLevelController>().ToList());
            // foreach (var gateLevel in Levels.Cast<GateLevelController>())
            // {
            //     gateLevel.OnLevelEnded += ContinueToNextLevel;
            //     gateLevel.gameObject.SetActive(false);
            // }
            // Levels.ElementAt(0).gameObject.SetActive(true);
        }
    }
}
