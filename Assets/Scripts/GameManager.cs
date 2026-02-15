using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] protected GameObject endParent;
        [SerializeField] protected GameObject nextButton;
        
        [SerializeField] internal string nodeID;
        [SerializeField] internal int stars = 0;
        [SerializeField] internal float time = 0;
        
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
            if (ProgressService.I != null)
            {
                nodeID = ProgressService.I.GetCurrentNodeID();
                if (ProgressService.I.Get(nodeID).bestStars > 0)
                {
                    skipTutorial = true;
                    stars = 1;
                }
            }
            
            GameState tutorialState = tutorialParent.AddComponent<TutorialState>();
            tutorialState.Init(tutorialParent);
            GameState minigameState = minigameParent.AddComponent<MinigameState>();
            minigameState.Init(minigameParent);
            GameState quizState = quizParent.AddComponent<QuizState>();
            quizState.Init(quizParent);
            GameState endState = gameObject.AddComponent<EndState>();
            endState.Init(gameObject);
            
            tutorialState.OnStateComplete += () =>
            {
                ChangeState(minigameState);
 
            };
            minigameState.OnStateStart += () =>
            {
                if(nextButton != null)
                    nextButton.SetActive(false);
            };
            minigameState.OnStateComplete += () => ChangeState(quizState);
            quizState.OnStateComplete += () => ChangeState(endState);
            
            ChangeState(skipTutorial ? minigameState : tutorialState);
        }

        public void AddStar()
        {
            stars++;
        }
    }
}
