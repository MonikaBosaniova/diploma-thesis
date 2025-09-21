using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStateMachine
{
    public class MinigameState : GameState
    {
        protected List<LevelController> Levels;
        protected GameObject LevelsParent;
        private int _currentLevelIndex;
        
        public override void Init(GameObject o)
        {
            base.Init(o);
            LevelsParent = o;
            stateObject = o;
            Levels = LevelsParent.GetComponentsInChildren<LevelController>().ToList();
        }
        
        public override void Enter()
        {
            base.Enter();
            foreach (var level in Levels)
            {
                level.OnLevelEnded += ContinueToNextLevel;
            }
            Levels.ElementAt(0).Init();
        }
        
        public override void Exit()
        {
            manager.AddStar();
            base.Exit();
        }

        public override void Update()
        {
        }

        protected void ContinueToNextLevel()
        {
            if (_currentLevelIndex == Levels.Count - 1)
            {
                Levels.ElementAt(_currentLevelIndex).Close();
                _currentLevelIndex = 0;
                OnStateComplete?.Invoke();
            }
            else
            {
                Levels.ElementAt(_currentLevelIndex).Close();
                if (_currentLevelIndex + 1 >= Levels.Count) return;
                Levels.ElementAt(_currentLevelIndex + 1).Init();
                _currentLevelIndex++;
            }
        }
    }
}
