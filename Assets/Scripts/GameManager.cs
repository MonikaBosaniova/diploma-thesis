using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentState;
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

            tutorialState.OnStateComplete += () => { ChangeState(minigameState); };
            minigameState.OnStateStart += () =>
            {
                if (nextButton != null)
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

        [ContextMenu("Hide All Levels")]
        public void HideAllLevels()
        {
            List<LevelController> levels = new  List<LevelController>();
            var tutorialLevels = tutorialParent.GetComponentsInChildren<LevelController>().ToList();
            var  minigameLevels = minigameParent.GetComponentsInChildren<LevelController>().ToList();
            
            levels.AddRange(tutorialLevels);
            levels.AddRange(minigameLevels);

            foreach (var level in levels)
            {
                for (int i = 0; i < level.transform.childCount; i++)
                {
                    var child = level.transform.GetChild(i);
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}

