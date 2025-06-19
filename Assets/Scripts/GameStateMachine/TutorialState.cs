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
        
        public TutorialState(GameObject obj) : base(obj)
        {
            TutorialsParent = obj;
            Tutorials = new List<LevelController>(TutorialsParent.GetComponents<LevelController>().ToList());
        }

        public override void Enter()
        {
            base.Enter();
            foreach (var tutorialStep in Tutorials)
            {
                tutorialStep.OnLevelEnded += ContinueToNextTutorial;
                tutorialStep.gameObject.SetActive(false);
            }
            Tutorials.ElementAt(0).gameObject.SetActive(true);
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }

        protected void ContinueToNextTutorial()
        {
            if (_currentTutorialIndex == Tutorials.Count - 1)
            {
                transform.GetChild(_currentTutorialIndex).gameObject.SetActive(false);
                _currentTutorialIndex = 0;
                OnStateComplete?.Invoke();
            }
            else
            {
                transform.GetChild(_currentTutorialIndex).gameObject.SetActive(false);
                if (_currentTutorialIndex + 1 >= Tutorials.Count) return;
                transform.GetChild(_currentTutorialIndex + 1).gameObject.SetActive(true);
                _currentTutorialIndex++;
            }
        }
    }
}
