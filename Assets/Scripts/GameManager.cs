using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        [FormerlySerializedAs("CurrentState")] 
        public GameState currentState;
        [SerializeField] internal bool skipTutorial = false;
        [SerializeField] internal bool gamePlayed = false;
        [SerializeField] protected GameObject tutorialParent;
        [SerializeField] protected GameObject minigameParent;
        [SerializeField] protected GameObject quizParent;
        [SerializeField] protected GameObject nextButton;
        [SerializeField] protected GameObject backButton;

        [SerializeField] internal string nodeID;
        [SerializeField] internal int stars = 0;
        [SerializeField] internal float time = 0;

        internal float StartTime;

        private void Start()
        {
            Initialization();
        }
        
        /// <summary>
        /// End current state and move to newState, call enter
        /// </summary>
        /// <param name="newState">new state to change</param>
        public void ChangeState(GameState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        /// <summary>
        /// Initialization of GameManager
        /// Load the progress
        /// </summary>
        protected virtual void Initialization()
        {
            //Load stars, if the state has stars, than the presentation will be skipped
            if (ProgressService.I != null)
            {
                nodeID = ProgressService.I.GetCurrentNodeID();
                if (ProgressService.I.Get(nodeID).bestStars > 0)
                {
                    if (ProgressService.I.Get(nodeID).bestStars >= 2)
                    {
                        gamePlayed = true;
                    }
                    skipTutorial = true;
                    stars = 1;
                }
            }
            
            //Add gameStates to its parents GameObjects and init them
            GameState tutorialState = tutorialParent.AddComponent<TutorialState>();
            tutorialState.Init(tutorialParent);
            GameState miniGameState = minigameParent.AddComponent<MinigameState>();
            miniGameState.Init(minigameParent);
            GameState quizState = quizParent.AddComponent<QuizState>();
            quizState.Init(quizParent);
            GameState endState = gameObject.AddComponent<EndState>();
            endState.Init(gameObject);
            
            //Creating the state machine Tutorial -> MiniGame -> Quiz -> End 
            tutorialState.OnStateComplete += () => { ChangeState(miniGameState); };
            miniGameState.OnStateStart += () =>
            {
                if (nextButton != null)
                    nextButton.SetActive(false);
                if (backButton != null)
                    backButton.SetActive(false);
            };
            miniGameState.OnStateComplete += () =>
            {
                gamePlayed = true;
                ChangeState(quizState);
            };
            quizState.OnStateComplete += () => ChangeState(endState);
            
            //If skipTutorial is true go to miniGame state right away
            ChangeState(skipTutorial ? miniGameState : tutorialState);
        }
        
        /// <summary>
        /// Increment the star count by one
        /// </summary>
        public void AddStar()
        {
            stars++;
        }

        /// <summary>
        /// Helping method to hide all children of all LevelControllers in the scene
        /// Called in ContextMenu in Editor
        /// </summary>
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
        
        /// <summary>
        /// Helping method to show all children of all LevelControllers in the scene
        /// Called in Context menu in Editor
        /// </summary>
        // [ContextMenu("Show All Levels")]
        // public void ShowAllLevels()
        // {
        //     List<LevelController> levels = new  List<LevelController>();
        //     var tutorialLevels = tutorialParent.GetComponentsInChildren<LevelController>().ToList();
        //     var  minigameLevels = minigameParent.GetComponentsInChildren<LevelController>().ToList();
        //     
        //     levels.AddRange(tutorialLevels);
        //     levels.AddRange(minigameLevels);
        //
        //     foreach (var level in levels)
        //     {
        //         for (int i = 0; i < level.transform.childCount; i++)
        //         {
        //             var child = level.transform.GetChild(i);
        //             child.gameObject.SetActive(true);
        //         }
        //     }
        // }
    }
}

