using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        protected GameState CurrentState;
        [SerializeField] private bool skipTutorial = false;
        [SerializeField] protected GameObject TutorialParent;
        [SerializeField] protected GameObject MinigameParent;
        
        private void Start()
        {
            Initialization();
        }
        
        public void ChangeState(GameState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        
        protected virtual void Initialization()
        {
            GameState tutorialState = new TutorialState(TutorialParent);
            GameState minigameState = new MinigameState(MinigameParent);
            
            tutorialState.OnStateComplete += () => ChangeState(minigameState);
            ChangeState(skipTutorial ? minigameState : tutorialState);
        }
    }
}

