using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStateMachine
{
    public class TutorialState : GameState
    {
        private List<LevelController> _tutorials;
        private GameObject _tutorialsParent;
        private int _currentTutorialIndex;
        
        public override void Init(GameObject o)
        {
            base.Init(o);
            _tutorialsParent = o;
            StateObject = o;
            _tutorials = _tutorialsParent.GetComponentsInChildren<LevelController>().ToList();
        }
        
        /// <summary>
        /// Enter tutorial state. Set actions to every step in tutorial
        /// And init the first step
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            foreach (var tutorialStep in _tutorials)
            {
                tutorialStep.OnLevelEnded += ContinueToNextTutorial;
                tutorialStep.OnGoBackInTutorial += GoBackInTutorial;
            }
            
            //Call init to first tutorial child (show)
            _tutorials.ElementAt(0).Init();
        }
        
        /// <summary>
        /// Exit tutorial state.
        /// Add star, calculated time, save and log progress
        /// </summary>
        public override void Exit()
        {
            Manager.AddStar();
            Manager.time = Time.time - Manager.StartTime;
            SaveProgress();
            LogProgress(this.name);
            StateObject.SetActive(false);
        }

        public override void Update()
        {
        }
        
        /// <summary>
        /// Checks if the next tutorial step is not on the end and invoke
        /// the next
        /// </summary>
        private void ContinueToNextTutorial()
        {
            if (_currentTutorialIndex == _tutorials.Count - 1)
            {
                _tutorials.ElementAt(_currentTutorialIndex).Close();
                _currentTutorialIndex = 0;
                OnStateComplete?.Invoke();
            }
            else
            {
                _tutorials.ElementAt(_currentTutorialIndex).Close();
                if (_currentTutorialIndex + 1 >= _tutorials.Count) return;
                _tutorials.ElementAt(_currentTutorialIndex + 1).Init();
                _currentTutorialIndex++;
            }
            
            Debug.Log("TUTORIAL NEXT: " + _currentTutorialIndex);
            
        }
        
        /// <summary>
        /// Checks if the current tutorial step is not the first one
        /// and call close on current and init to next (actually before)
        /// </summary>
        private void GoBackInTutorial()
        {
            _tutorials.ElementAt(_currentTutorialIndex).Close();
            if (_currentTutorialIndex < 1) return;
            _tutorials.ElementAt(_currentTutorialIndex - 1).Init();
            _currentTutorialIndex--;
            
            Debug.Log("TUTORIAL BACK: " + _currentTutorialIndex);
        }
    }
}
