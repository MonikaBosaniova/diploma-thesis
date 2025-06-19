using System.Collections.Generic;
using UnityEngine;

namespace GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
    
        private GameState currentState;
        [SerializeField] private bool skipTutorial = false;
        [SerializeField] protected GameObject TutorialParent;
        [SerializeField] protected GameObject LevelsParent;

        protected List<LevelController> Tutorials;
        protected List<LevelController> Levels;

        private int _currentLevelIndex;
        private int _currentTutorialIndex;
        

        private void Start()
        {
            Initialization();
        }
        
        void Update()
        {
            currentState?.Update();
        }
        
        public void ChangeState(GameState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        protected virtual void Initialization()
        {
            _currentLevelIndex = 0;
            if (!skipTutorial)
            {
                //InvokeOnTutorialStarted();
                //Tutorials = FindObjectsByType<LevelController>()
            }
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
    
        protected void ContinueToNextTutorial()
        {
            if (_currentTutorialIndex == Tutorials.Count - 1)
            {
                transform.GetChild(_currentTutorialIndex).gameObject.SetActive(false);
                _currentTutorialIndex = 0;
                //InvokeOnTutorialEnded();
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

