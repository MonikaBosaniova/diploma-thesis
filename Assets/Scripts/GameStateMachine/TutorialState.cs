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
            base.Init(o);
            TutorialsParent = o;
            stateObject = o;
            Tutorials = TutorialsParent.GetComponentsInChildren<LevelController>().ToList();
        }
        
        public override void Enter()
        {
            base.Enter();
            foreach (var tutorialStep in Tutorials)
            {
                tutorialStep.OnLevelEnded += ContinueToNextTutorial;
                tutorialStep.OnGoBackInTutorial += GoBackInTutorial;
            }
            Tutorials.ElementAt(0).Init();
        }

        public override void Exit()
        {
            manager.AddStar();
            base.Exit();
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
            Debug.Log("TUTORIAL GO: " + _currentTutorialIndex);
            
        }
        
        protected void GoBackInTutorial()
        {
            Tutorials.ElementAt(_currentTutorialIndex).Close();
            if (_currentTutorialIndex < 1) return;
            Tutorials.ElementAt(_currentTutorialIndex - 1).Init();
            _currentTutorialIndex--;
            Debug.Log("TUTORIAL BACK: " + _currentTutorialIndex);
        }
    }
}
