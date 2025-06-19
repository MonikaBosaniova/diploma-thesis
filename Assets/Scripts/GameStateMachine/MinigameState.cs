using System.Collections.Generic;
using UnityEngine;

namespace GameStateMachine
{
    public class MinigameState : GameState
    {
        protected List<LevelController> Levels;
        private int _currentLevelIndex;
    
        public MinigameState(GameObject obj) : base(obj) { }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        protected void ContinueToNextLevel()
        {
            if (_currentLevelIndex == Levels.Count - 1)
            {
                transform.GetChild(_currentLevelIndex).gameObject.SetActive(false);
                _currentLevelIndex = 0;
                //InvokeOnMiniGameEnded();
            }
            else
            {
                transform.GetChild(_currentLevelIndex).gameObject.SetActive(false);
                if (_currentLevelIndex + 1 >= Levels.Count) return;
                transform.GetChild(_currentLevelIndex + 1).gameObject.SetActive(true);
                _currentLevelIndex++;
            }
        }
    }
}
