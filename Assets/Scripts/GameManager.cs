using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        protected GameState CurrentState;
        [SerializeField] private bool skipTutorial = false;
        [SerializeField] protected GameObject tutorialParent;
        [SerializeField] protected GameObject minigameParent;
        [SerializeField] protected GameObject quizParent;
        
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
            GameState tutorialState = tutorialParent.AddComponent<TutorialState>();
            tutorialState.Init(tutorialParent);
            GameState minigameState = minigameParent.AddComponent<MinigameState>();
            minigameState.Init(minigameParent);
            
            tutorialState.OnStateComplete += () => ChangeState(minigameState);
            ChangeState(skipTutorial ? minigameState : tutorialState);
        }
    }
}

