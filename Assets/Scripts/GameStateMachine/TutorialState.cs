using System;
using System.Collections.Generic;
using System.Linq;
using Gates;
using UnityEngine;

namespace GameStateMachine
{
    public class TutorialState : GameState
    {
        protected List<LevelController> Tutorials;
        protected GameObject TutorialsParent;
        private int _currentTutorialIndex;
        
        public override void Init(GameObject o)
        {
            TutorialsParent = o;
            stateObject = o;
            Tutorials = TutorialsParent.GetComponentsInChildren<LevelController>().ToList();
            Debug.Log(Tutorials.Count + " Tutorials loaded");
        }
        
        public override void Enter()
        {
            base.Enter();
            foreach (var tutorialStep in Tutorials)
            {
                tutorialStep.OnLevelEnded += ContinueToNextTutorial;
            }
            Tutorials.ElementAt(0).Init();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }

        protected void ContinueToNextTutorial()
        {
            if (_currentTutorialIndex == Tutorials.Count - 1)
            {
                Tutorials.ElementAt(_currentTutorialIndex).Close();
                _currentTutorialIndex = 0;
                OnStateComplete?.Invoke();
            }
            else
            {
                Tutorials.ElementAt(_currentTutorialIndex).Close();
                if (_currentTutorialIndex + 1 >= Tutorials.Count) return;
                Tutorials.ElementAt(_currentTutorialIndex + 1).Init();
                _currentTutorialIndex++;
            }
        }
    }
}
